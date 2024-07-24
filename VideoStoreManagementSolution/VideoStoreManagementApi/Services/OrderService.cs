
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
        private readonly IRefundRepository _refundRepository;
        private readonly IGeoLocationServices _geoLocationServices;

        #region Constructor
        public OrderService(ICartRepository cartRepository, ICartItemsRepository cartItemsRepository, IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IPaymentRepository paymentRepository, ITokenService tokenService, IAddressRepository addressRepository, ICustomerRepository customerRepository, IRentalRepository rentalRepository, IPermanentRepository permanentRepository, IVideoRepository videoRepository, IInventoryRepository inventoryRepository, VideoStoreContext dbContext, IDTOService dTOService, IRefundRepository refundRepository, IGeoLocationServices geoLocationServices)
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
            _videoRepository = videoRepository;
            _inventoryRepository = inventoryRepository;
            _dbContext = dbContext;
            _dTOService = dTOService;
            _refundRepository = refundRepository;
            _geoLocationServices = geoLocationServices;
        }
        #endregion

        #region MakePayment
        /// <summary>
        /// Handles Payment,places order, delete cart items
        /// </summary>
        /// <param name="paymentType"></param>
        /// <param name="addressId"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoItemsInCartException"></exception>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="QunatityOutOfStockException"></exception>
        /// <exception cref="DbException"></exception>
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
            if (res != string.Empty) { throw new QunatityOutOfStockException(res + " These CarItemId  are Out Of Stock"); }

            MembershipType membershipType = await _customerRepository.GetMembershipType((int)uid);


            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {

                    var order = await OrderHelper.CreateOrder((int)uid, addressId, paymentType, membershipType);
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
                        payment.Amount = (float)(cart.TotalPrice + (0.05 * cart.TotalPrice));
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
                    await ClearCartAndCartItems(cartItems.ToList(), cart);

                    var orderItems = await _orderItemRepository.GetOrderItemsbyOrderId(order.Id);
                    var address = await _addressRepository.GetById(addressId);

                    var dto = _dTOService.MapOrderToOrderDTO(order, orderItems.ToList(), address, payment);
                    await transaction.CommitAsync();
                    return dto;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DbException("Error Occured which creating Order" + ex);

                }
            }

        }
        #endregion

        #region Helpers
        
        /// <summary>
        /// Before making payment checks items with Qunatity required in cart are in stock 
        /// </summary>
        /// <param name="cartItems"></param>
        /// <returns></returns>
        public async Task<string> CheckStockOfVideos(IList<CartItem> cartItems)
        {
            string ans = string.Empty;
            foreach (var cartItem in cartItems)
            {
                var stock = await _inventoryRepository.GetQty(cartItem.VideoId);
                if (cartItem.Qty > stock)
                {
                    ans += cartItem.Id;
                }
            }
            return ans;
        }
      
        /// <summary>
        ///  Transfer them to OrderItems
        /// </summary>
        /// <param name="cartItems"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task CartItemsToOrderItems(IList<CartItem> cartItems, int orderId)
        {
            foreach (var cartItem in cartItems)
            {
                OrderItem orderItem = new OrderItem() { OrderId = orderId, Price = cartItem.Price, Qty = cartItem.Qty, VideoId = cartItem.VideoId };
                await _orderItemRepository.Add(orderItem);
            }
            return;
        }
       
        /// <summary>
        /// Clear Cart and CartItems
        /// </summary>
        /// <param name="cartItems"></param>
        /// <param name="cart"></param>
        /// <returns></returns>
        public async Task ClearCartAndCartItems(IList<CartItem> cartItems, Cart cart)
        {
            foreach (var cartItem in cartItems)
            {
                await _cartItemsRepository.Delete(cartItem.Id);
            }
            await _cartRepository.Delete(cart.Id);
        }

        /// <summary>
        /// Updates the stock after placing order
        /// </summary>
        /// <param name="cartItems"></param>
        /// <returns></returns>
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
        #endregion

        #region GetOrdersOfUser
        /// <summary>
        /// Fetches all orders of users
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoSuchItemInDbException"></exception>
        public async Task<IList<OrderDTO>> GetOrdersOfUser()
        {
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var orders = await _orderRepository.GetOrdersByUid((int)uid);
            if (orders == null) throw new NoSuchItemInDbException("No Orders Placed by the Customer");
            IList<OrderDTO> orderDTOList = new List<OrderDTO>();
            foreach (var order in orders)
            {
                Payment? payment = await _paymentRepository.GetPaymentByOrderId(order.Id);
                orderDTOList.Add(_dTOService.MapOrderToOrderDTO(order, order.OrderItems.ToList(), order.DeliveryAddress, payment));
            }
            return orderDTOList;

        }
        #endregion

        #region GetOrderById
        /// <summary>
        /// Fetches an order by order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>OrderDTO</returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<OrderDTO> GetOrderById(int orderId)
        {
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var order = await _orderRepository.GetById(orderId);
            if (order == null) throw new NoSuchItemInDbException("No Order With Given Id");
            if (order.CustomerId != (int)uid) throw new UnauthorizedAccessException("You cant Acess the Given Order");
            var orderItems = await _orderItemRepository.GetOrderItemsbyOrderId(orderId);
            Payment? payment = await _paymentRepository.GetPaymentByOrderId(orderId);
            return _dTOService.MapOrderToOrderDTO(order, orderItems.ToList(), order.DeliveryAddress, payment);
        }
        #endregion

        #region ChangeOrderStatusForAdmin
        /// <summary>
        /// Changes order status Admin Service
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="deliveryStatus"></param>
        /// <returns></returns>
        /// <exception cref="NoSuchItemInDbException"></exception>
        public async Task<OrderStatus> ChangeOrderStatusForAdmin(int OrderId, OrderStatus deliveryStatus)
        {
            var order = await _orderRepository.GetById(OrderId);
            if (order == null)
            {
                throw new NoSuchItemInDbException("The Order With Given Id Doesnt Eixst");
            }
            order.OrderStatus = deliveryStatus;
            await _orderRepository.Update(order);
            return deliveryStatus;
        }
        #endregion

        #region MarkPaymentDoneCodForAdmin
        /// <summary>
        /// Changes Payment Status For COD
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        /// <exception cref="NoSuchItemInDbException"></exception>
        public async Task<MessageDTO> MarkPaymentDoneCodForAdmin(int OrderId)
        {
            var order = await _orderRepository.GetById(OrderId);
            if (order == null) { throw new NoSuchItemInDbException("The Order With Given Id Doesnt Eixst"); }
            order.PaymentDone = true;
            return new MessageDTO() { Message = "Payment Changed Sucessfully" };
        }
        #endregion

        #region CancelOrder
        /// <summary>
        /// Cancels Order , if payment done it issues refund
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>Returns a Message </returns>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="OrderCantBeCancelledException"></exception>
        /// <exception cref="DbException"></exception>
        public async Task<MessageDTO> CancelOrder(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            if (order == null)
            {
                throw new NoSuchItemInDbException("Please Check OrderId and retry");
            }
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            if (order.CustomerId != uid)
            {
                throw new UnauthorizedAccessException("You dont Have previllage To acess the order");
            }
            if (order.OrderStatus == OrderStatus.Delivered || order.OrderStatus == OrderStatus.OutForDelivery || order.OrderStatus == OrderStatus.Canceled)
            {
                throw new OrderCantBeCancelledException($"Order Cant be Cancelled , because of order is {order.OrderStatus}");
            }
            if (order.PaymentType == PaymentType.COD) {
                order.OrderStatus = OrderStatus.Canceled;
                await _orderRepository.Update(order);
                return new MessageDTO() { Message = "Order Cancelled Sucessfully" }; 
            }
            var payment = order.Payments.SingleOrDefault(p => p.PaymentSucess = true);
            if (payment == null)
            {
                throw new DbException("Cant Fetch Payment Details");
            }
            Refund refund = new Refund()
            {
                Amount = (float)(order.TotalAmount - (0.1 * order.TotalAmount)),
                OrderId = orderId,
                Status = RefundStatus.Intiated,
                TranasactionId = payment.TransactionId,
            };
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    refund = await _refundRepository.Add(refund);
                    await transaction.CommitAsync();
                } catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new DbException("Refud Cant be Initated" + ex.Message);
                }
            }
            order.OrderStatus = OrderStatus.Canceled;
            await _orderRepository.Update(order);
            return new MessageDTO() { Message = "Refund Intiated Sucessfully" };
        }
        #endregion

        #region CheckRefundStatus
        /// <summary>
        /// Used to check refund status of order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>RefundDTO</returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="NoSuchItemInDbException"></exception>
        public async Task<RefundDTO> CheckRefundStatus(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            if (order.CustomerId != (int)uid) throw new UnauthorizedAccessException("You dont have access to this order");
            if (order == null) throw new NoSuchItemInDbException("Please check order id");
            var refund = await _refundRepository.GetRefundByOrderId(orderId);
            if (refund == null) throw new NoSuchItemInDbException("Please check order status , you didnt cancelled the order or made payment type as cod");

            return _dTOService.MapRefundToRefundDTO(refund);

        }
        #endregion

        #region IssuseRefundForAdmin
        /// <summary>
        /// Issues Refund to an order Admin Feature
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="NoSuchItemInDbException"></exception>
        public async Task<MessageDTO> IssuseRefundForAdmin(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            if (order == null) throw new NoSuchItemInDbException("Please check order id");
            var refund = await _refundRepository.GetRefundByOrderId(orderId);
            if (refund == null) throw new NoSuchItemInDbException("Please check order status , you didnt cancelled the order or made payment type as cod");
            refund.Status = RefundStatus.Refunded;
            await _refundRepository.Update(refund);
            return new MessageDTO() { Message = "Changed Refund Status Sucesfully" };
        }
        #endregion

        #region ViewAllRefundsForAdmin
        /// <summary>
        /// Fetches All Refunds Admin Feture
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Refund>> ViewAllRefundsForAdmin()
        {
            var refunds = await _refundRepository.GetAll();
            return refunds.ToList();
        }
        #endregion

        #region ViewAllOrdersAreBasedOnStatusdForAdmin
        /// <summary>
        /// Fetches Order based on status for admin
        /// </summary>
        /// <param name="orderStatus"></param>
        /// <returns>A List Orders</returns>
        public async Task<IList<Order>> ViewAllOrdersAreBasedOnStatusdForAdmin(OrderStatus orderStatus)
        {
            var orders = await _orderRepository.GetAll();
            var res = orders.Where(o => o.OrderStatus == orderStatus).ToList();
            return res;
        }
        #endregion

        #region GetOrderbyIdForAdmin
        /// <summary>
        /// Fetches an Order by id Admin Feature
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        /// <exception cref="NoSuchItemInDbException"></exception>
        public async Task<Order> GetOrderbyIdForAdmin(int orderId)
        {
            var order = await _orderRepository.GetById(orderId);
            if (order == null) throw new NoSuchItemInDbException("No Such Order Exist");
            return order;
        }
        #endregion

        #region GetAllOrdersForAdmin
        /// <summary>
        /// Fetches all Orders for admin with Pagination 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<IList<Order>> GetAllOrdersForAdmin(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0) throw new ArgumentException("Invalid pagenumber,pagesize");
            var order = await _orderRepository.GetAll();
            order = order.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(o => o.Id);
            return order.ToList();
        }
        #endregion

        #region ReturnVideos
        public async Task<Rental> ReturnVideos(int orderId)
        {
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }

            var rental = await _rentalRepository.GetByOrderId(orderId);
            if (rental == null)
            {
                throw new NullReferenceException("Rental not found");
            }

            rental.ReturnDate = DateTime.Now;

            if (rental.ReturnDate > rental.DueDate)
            {
                var lateHours = (rental.ReturnDate - rental.DueDate).TotalHours;
                var lateFee = (float)(Math.Ceiling(lateHours) * 5);
                rental.LateFee = lateFee;
            }
            else
            {
                rental.LateFee = 0;
            }

           rental =  await _rentalRepository.Update(rental);

            return rental;
        }
        #endregion
    }
}
