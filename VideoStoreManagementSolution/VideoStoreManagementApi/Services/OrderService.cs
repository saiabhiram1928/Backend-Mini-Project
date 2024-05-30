
using System.Collections.Generic;
using System.Data;
using System.Security.AccessControl;
using System.Security.Cryptography.Xml;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Models.Enums;
using VideoStoreManagementApi.Repositories;
using VideoStoreManagementApi.Services.Helpers;

namespace VideoStoreManagementApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemsRepository _cartItemsRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ITokenService _tokenService;
        private readonly IAddressRepository _addressRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IPermanentRepository _permanentRepository;
        private readonly IVideoRepository _videoRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly VideoStoreContext _dbContext;
        private readonly IDTOService _dTOService;

        public OrderService(ICartRepository cartRepository , ICartItemsRepository cartItemsRepository, IOrderItemRepository orderItemRepository , IOrderRepository orderRepository , IPaymentRepository paymentRepository, ITokenService tokenService,IAddressRepository addressRepository ,ICustomerRepository customerRepository , IRentalRepository rentalRepository, IPermanentRepository permanentRepository , IVideoRepository videoRepository , IInventoryRepository inventoryRepository , VideoStoreContext dbContext , IDTOService dTOService  )
        {
            _cartRepository = cartRepository;
            _cartItemsRepository = cartItemsRepository;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _tokenService = tokenService;
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
            _permanentRepository = permanentRepository;
            _videoRepository  = videoRepository;
            _inventoryRepository = inventoryRepository;
            _dbContext = dbContext;
            _dTOService = dTOService;
        }
        public async Task<OrderDTO> MakePayment(PaymentType paymentType, int addressId)
        {
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }

            var cart = await _cartRepository.GetByUserId((int)uid);
            if (cart == null) throw new NoItemsInCartException();
            var cartItems = await _cartItemsRepository.GetCartItemsWithCartId(cart.Id);
            if (cartItems == null) throw new NoItemsInCartException();
           var addressCheck = await _addressRepository.CheckAddressIsOfUser((int)uid, addressId);
            if (!addressCheck) throw new NoSuchItemInDbException("Please Check Address Id Or It is not in your address list");
            var res = await CheckStockOfVideos(cartItems.ToList());
            if (res!=string.Empty) { throw new QunatityOutOfStockException(res+" These CarItemId  are Out Of Stock"); }

            MembershipType membershipType =  await _customerRepository.GetMembershipType((int)uid);


            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {

                    var order = OrderHelper.CreateOrder((int)uid, addressId, paymentType, membershipType);
                    order.TotalAmount = cart.TotalPrice;

                    order = await _orderRepository.Add(order);

                    int totalQty = cartItems.Sum(ci => ci.Qty);

                    Payment payment = null;
                    if (paymentType == PaymentType.UPI)
                    {



                        payment = new Payment();
                        payment.PaymentDate = DateTime.Now;
                        payment.PaymentSucess = true;
                        payment.OrderId = order.Id;
                        payment.TransactionId = OrderHelper.GenerateTransactionId();
                        payment.Amount = cart.TotalPrice;
                        order.PaymentDone = true;
                        order = await _orderRepository.Update(order);
                        payment = await _paymentRepository.Add(payment);


                    }

                    if (membershipType == MembershipType.Basic)
                    {

                        Rental rental = new Rental();
                        rental.TotalQty = totalQty;
                        rental.OrderId = order.Id;
                        rental.RentDate = DateTime.Now;
                        rental.DueDate = order.ExpectedDeliveryDate.AddDays(7);
                        rental.TotalPrice = cart.TotalPrice;
                        rental = await _rentalRepository.Add(rental);



                    }
                    else
                    {

                        Permanent permanent = new Permanent();
                        permanent.TotalQty = totalQty;
                        permanent.TotalPrice = cart.TotalPrice;
                        permanent.OrderId = order.Id;
                        permanent.TotalPrice = cart.TotalPrice;
                        permanent = await _permanentRepository.Add(permanent);


                    }

                    await ManageStock(cartItems.ToList());
                    await CartItemsToOrderItems(cartItems.ToList(), order.Id);
                    await ClearCartAndCartItems(cartItems.ToList() ,cart);

                    var orderItems = await _orderItemRepository.GetOrderItemsbyOrderId(order.Id);
                    var address = await _addressRepository.GetById(addressId);
                   
                    var dto= _dTOService.MapOrderToOrderDTO(order, orderItems.ToList(), address, payment);
                    await transaction.CommitAsync();
                    return dto;
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DbException("Error Occured which creating Order" + ex);
                    
                }
            }

        }
        public async Task<string> CheckStockOfVideos(IList<CartItem> cartItems)
        {
            string ans = string.Empty;
            foreach(var  cartItem in cartItems)
            {
                var stock = await _inventoryRepository.GetQty(cartItem.VideoId);
                if (cartItem.Qty > stock)
                {
                    ans += cartItem.Id;
                }
            }
            return ans;
        }
        public async Task CartItemsToOrderItems(IList<CartItem> cartItems , int orderId)
        {
            foreach (var  cartItem in cartItems)
            {
                OrderItem orderItem = new OrderItem() { OrderId = orderId , Price = cartItem.Price , Qty = cartItem.Qty , VideoId = cartItem.VideoId};
                await _orderItemRepository.Add(orderItem);
            }
            return;
        }
        public async Task ClearCartAndCartItems(IList<CartItem> cartItems , Cart cart)
        {
            foreach(var cartItem in cartItems)
            {
                await _cartItemsRepository.Delete(cartItem.Id);
            }
            await _cartRepository.Delete(cart.Id);
        }
        public async Task ManageStock(IList<CartItem> cartItems)
        {
         
            foreach (var cartItem in cartItems)
                     {
                        var inventory = await _inventoryRepository.GetById(cartItem.VideoId);
                        inventory.Stock -= cartItem.Qty;
                        await _inventoryRepository.Update(inventory);
                    }
            return;
            
        }

        public async Task<IList<OrderDTO>> GetOrdersOfUser()
        {
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var orders = await _orderRepository.GetOrdersByUid((int)uid);
            if (orders == null) throw new NoSuchItemInDbException("No Orders Placed by the Customer");
             IList < OrderDTO > orderDTOList = new List<OrderDTO>();
           foreach (var order in orders)
            {
                Payment ? payment = await _paymentRepository.GetPaymentByOrderId(order.Id);
                orderDTOList.Add(_dTOService.MapOrderToOrderDTO(order, order.OrderItems.ToList(),order.DeliveryAddress, payment));
            }
           return orderDTOList;

        }

        public async Task<OrderDTO> GetOrderById(int orderId)
        {
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var order =await _orderRepository.GetById(orderId);
            if (order == null) throw new NoSuchItemInDbException("No Order With Given Id");
            if (order.CustomerId != (int)uid) throw new UnauthorizedAccessException("You cant Acess the Given Order");
            Payment ? payment = await _paymentRepository.GetPaymentByOrderId(orderId);
            return _dTOService.MapOrderToOrderDTO(order, order.OrderItems.ToList(), order.DeliveryAddress, payment);
        }
        public async Task<DeliveryStatus> ChangeDeliveryStatus(int OrderId, DeliveryStatus deliveryStatus)
        {
            var order = await _orderRepository.GetById(OrderId);
            if (order == null)
            {
                throw new NoSuchItemInDbException("The Order With Given Id Doesnt Eixst");
            }
            order.DeliveryStatus = deliveryStatus;
            await _orderRepository.Update(order);
            return deliveryStatus;
        }

        public async Task<string> MarkPaymentDoneCod(int OrderId)
        {
            var order = await _orderRepository.GetById(OrderId);
            if (order == null) { throw new NoSuchItemInDbException("The Order With Given Id Doesnt Eixst"); }
            order.PaymentDone = true;
            return "Sucessfully Updated";
        }
    }
}
