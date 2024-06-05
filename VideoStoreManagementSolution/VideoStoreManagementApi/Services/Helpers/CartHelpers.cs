using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Services.Helpers
{
    public class CartHelpers
    {
       
        public Cart CreateCart(int uid)
        {

            Cart cart = new Cart() { CustomerId = (int)uid };
           
            return cart;
        }
        public CartItem CreateCartItem(int videoId, int cartId)
        {
            CartItem cartItem = new CartItem();
            cartItem.VideoId = videoId;

            cartItem.CartId = cartId;
            return cartItem;
        }
        public float CalculateTotalPrice(IList<CartItem> list)
        {
            float totalPrice = 0;
            foreach (var item in list)
            {
                totalPrice += item.Price;
            }
            return totalPrice;
        }
    }
}
