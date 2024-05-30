using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.Interfaces.Services
{
    public interface ICartService
    {
        public Task<CartDTO> AddToCart(int videoId, int qty);
        public  Task<CartDTO> ViewCart();
        public Task<CartDTO> EditCart(int cartItemId, int newQty);
        public Task<CartDTO> DeleteCartItem(int cartItemId);
    }
}
