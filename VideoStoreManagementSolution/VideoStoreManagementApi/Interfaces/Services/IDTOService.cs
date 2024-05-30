using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Services
{
    public interface IDTOService
    {
        public UserReturnDTO MapUserToUserReturnDTO(Customer user, string token);
        public User MapUserRegisterDTOToUser(UserRegisterDTO userRegisterDTO, byte[] salt, byte[] passwd);
        public Customer MapUserRegisterDTOToCustomer(UserRegisterDTO userRegisterDTO, User user);
        public Video MapVideoRegisterDTOToVideo(VideoRegisterDTO videoRegisterDTO);
        public AddressDTO MapAddressToAddressDTO(Address a);
        public Address MapAddressDTOToAddress(AddressDTO addressDTO, Address address);
        public AddressDTO MapAddressRegisterDTOTOAddressDTO(AddressRegisterDTO addressRegisterDTO);
        public OrderDTO MapOrderToOrderDTO(Order order, IList<OrderItem> orderItems, Address address, Payment? payment);

        public CartDTO MapCartToCartDTO(Cart cart, IList<CartItem> cartItems);
        public VideoDTO MapVideoToVideoDTO(Video video, int stock);
    }
}
