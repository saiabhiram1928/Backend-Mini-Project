using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface ICartItemsRepository : IRepository<int,CartItem>
    {
        public Task<bool> CheckCartItemExist(int videoId , int cartId);
        public Task<CartItem> GetCartItemWithVideoId(int videoId , int cartId);
        public Task<IEnumerable<CartItem>> GetCartItemsWithCartId(int cartId);


    }
}
