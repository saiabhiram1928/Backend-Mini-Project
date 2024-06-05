using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.Services
{
    public class DTOService : IDTOService
    {
        public OrderDTO MapOrderToOrderDTO(Order order, IList<OrderItem> orderItems ,Address address , Payment? payment)
        {
            OrderDTO orderDTO = new OrderDTO()
            {
                DeliveryAddress = address,
                Items = orderItems,
                CustomerId = order.CustomerId,
                OrderStatus
                = order.OrderStatus,
                ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                OrderedDate = order.OrderedDate,
                OrderId = order.Id,
                PaymentDone = order.PaymentDone,
                RealDeliveredDate = order.RealDeliveredDate,
                RentalOrPermanent = order.RentalOrPermanent,
                Amount = order.TotalAmount,
                PaymentType = order.PaymentType, 

            };
            if(payment != null ) { orderDTO.TransactionId = payment.TransactionId; orderDTO.PaymentDate = payment.PaymentDate; }
            return orderDTO;    
            
        }
        public Address MapAddressDTOToAddress(AddressDTO addressDTO, Address a)
        {
            a.Area = addressDTO.Area;
            a.City = addressDTO.City;
            a.PrimaryAdress = addressDTO.PrimaryAdress;
            a.Zipcode = addressDTO.Zipcode;
            a.State = addressDTO.State;
            return a;

        }

        public AddressDTO MapAddressRegisterDTOTOAddressDTO(AddressRegisterDTO addressRegisterDTO)
        {
            AddressDTO addressDTO = new AddressDTO();
            addressDTO.Area = addressRegisterDTO.Area;
            addressDTO.City = addressRegisterDTO.City;
            addressDTO.PrimaryAdress = addressRegisterDTO.PrimaryAdress;
            addressDTO.Zipcode = addressRegisterDTO.Zipcode;
            addressDTO.State = addressRegisterDTO.State;
            return addressDTO;
        }

        public AddressDTO MapAddressToAddressDTO(Address a)
        {
          AddressDTO addressDTO = new AddressDTO();
            addressDTO.Area = a.Area;
            addressDTO.City = a.City;
            addressDTO.PrimaryAdress = a.PrimaryAdress;
            addressDTO.Zipcode = a.Zipcode;
            addressDTO.Id = a.Id;
            addressDTO.State = a.State;
            return addressDTO;
        }

        public Customer MapUserRegisterDTOToCustomer(UserRegisterDTO userRegisterDTO, User user)
        {
            Customer customer = new Customer();
            customer.MembershipType = Models.Enums.MembershipType.Basic;
            customer.Uid = user.Uid;
            
            return customer;
        }

        public User MapUserRegisterDTOToUser(UserRegisterDTO userRegisterDTO, byte[] salt, byte[] passwd)
        {
            User user = new User();
         
            user.FirstName = userRegisterDTO.FirstName;
            user.LastName = userRegisterDTO.LastName;
            user.Password = passwd;
            user.Salt = salt;
            user.Verified = true;
            user.Email = userRegisterDTO.Email;
           user.Role = Models.Enums.Role.Customer;
            return user;
           
        }

        public UserReturnDTO MapUserToUserReturnDTO(Customer user, string Token)
        {
            UserReturnDTO userReturnDTO = new UserReturnDTO();
            userReturnDTO.FirstName = user.User.FirstName;
            userReturnDTO.LastName = user.User.LastName;
            userReturnDTO.Email = user.User.Email;
            userReturnDTO.Verified = user.User.Verified;
            userReturnDTO.MembershipType = user.MembershipType;
            userReturnDTO.Role = user.User.Role;
            userReturnDTO.Token = Token;
            return userReturnDTO;
        }

        public Video MapVideoRegisterDTOToVideo(VideoRegisterDTO videoRegisterDTO)
        {
           Video video = new Video();   
           video.Tittle = videoRegisterDTO.Tittle;
            video.Price = videoRegisterDTO.Price;
            video.Description = videoRegisterDTO.Description;
            video.Director = videoRegisterDTO.Director;
            video.Genre = videoRegisterDTO.Genre;
            video.ReleaseDate = videoRegisterDTO.ReleaseDate;
            return video;
        }
        public CartItemDTO MapCartItemToCartItemDTO(CartItem cartItem)
        {
            CartItemDTO cartItemDTO = new CartItemDTO();
            cartItemDTO.CartId = cartItem.CartId;
            cartItemDTO.CartItemId = cartItem.Id;
            cartItemDTO.VideoId = cartItem.VideoId;
            cartItemDTO.VideoTittle = cartItem.Video.Tittle;
            cartItemDTO.Qty = cartItem.Qty;
            cartItemDTO.Price = cartItem.Price;
            return cartItemDTO;
        }
        public CartDTO MapCartToCartDTO(Cart cart, IList<CartItem> cartItems)
        {
            CartDTO cartDTO = new CartDTO();
            cartDTO.cartId = cart.Id;
            cartDTO.customerId = cart.CustomerId;
            cartDTO.TotalPrice = cart.TotalPrice;
            foreach(var cartItem in cartItems)
            {
                cartDTO.CartItems.Add(MapCartItemToCartItemDTO(cartItem));
            }
            return cartDTO;
        }

        public VideoDTO MapVideoToVideoDTO(Video video, int stock)
        {
           VideoDTO videoDTO = new VideoDTO() { Id = video.Id  , Description = video.Description , Director = video.Director , Genre = video.Genre , Price = video.Price , ReleaseDate = video.ReleaseDate , Stock = stock , Tittle = video.Tittle};

            return videoDTO;
           
        }

        public RefundDTO MapRefundToRefundDTO(Refund refund)
        {
            RefundDTO refundDTO = new RefundDTO();
            refundDTO.OrderId = refund.OrderId;
            refundDTO.TranasactionId = refund.TranasactionId;
            refundDTO.Amount = refund.Amount;
            refundDTO.Status = refund.Status;
            refundDTO.Id = refund.Id;
            return refundDTO;
        }
    }
}
