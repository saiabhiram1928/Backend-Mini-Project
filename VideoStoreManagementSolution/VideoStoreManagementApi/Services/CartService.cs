using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Cryptography.Xml;
using System.Transactions;
using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Repositories;
using VideoStoreManagementApi.Services.Helpers;

namespace VideoStoreManagementApi.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemsRepository _cartItemsRepository;
        private readonly ITokenService _tokenService;
        private readonly IVideoRepository _videoRepository;
        private readonly IDTOService _dTOService;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly CartHelpers _cartHelpers = new CartHelpers();

        #region Constructor
        public CartService(ICartRepository cartRepository, ICartItemsRepository cartItemsRepository, ITokenService tokenService, IVideoRepository videoRepository, IDTOService dTOService , IInventoryRepository inventoryRepository)
        {
            _cartRepository = cartRepository;
            _cartItemsRepository = cartItemsRepository;
            _tokenService = tokenService;
            _videoRepository = videoRepository;
            _dTOService = dTOService;
            _inventoryRepository = inventoryRepository;
        }

        #endregion

        #region AddToCart
        /// <summary>
        /// Adds Item to cart, if cart doenst exist it creates cart and cartitems repsetively
        /// </summary>
        /// <param name="videoId"></param>
        /// <param name="qty"></param>
        /// <returns>CartDTO</returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="QunatityOutOfStockException"></exception>
        public async Task<CartDTO> AddToCart(int videoId, int qty)
        {
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            
                    Cart cart = await _cartRepository.GetByUserId((int)uid);
                    if (cart == null)
                    {
                        cart = _cartHelpers.CreateCart((int)uid);
                        cart = await _cartRepository.Add(cart);
                    }
                    var videoExists = await _videoRepository.CheckVideoExistById(videoId);
                      
                    if (!videoExists)
                    {
                        throw new NoSuchItemInDbException("The video with given Id doenst exist");
                    }
                    var eachVideoPrice = await _videoRepository.GetPriceOfVideo(videoId);
                    var existingQty = await _inventoryRepository.GetQty(videoId);
                 
                    var presentCartItem = await _cartItemsRepository.GetCartItemWithVideoId(videoId, cart.Id);
                    if (presentCartItem == null)
                    {
                        presentCartItem = _cartHelpers.CreateCartItem(videoId, cart.Id);
                    if (existingQty < presentCartItem.Qty+qty) { throw new QunatityOutOfStockException(); }
                    presentCartItem = await _cartItemsRepository.Add(presentCartItem);
                    }
            if (existingQty < presentCartItem.Qty + qty) { throw new QunatityOutOfStockException(); }
            presentCartItem.Qty += qty;
                   
                    presentCartItem.Price = (presentCartItem.Qty) * eachVideoPrice;
                    presentCartItem = await _cartItemsRepository.Update(presentCartItem);

                    var cartItems = await _cartItemsRepository.GetCartItemsWithCartId(cart.Id);

                    cart.TotalPrice = _cartHelpers.CalculateTotalPrice(cartItems.ToList());
                    cart = await _cartRepository.Update(cart);
                    

                    var cartDTO = _dTOService.MapCartToCartDTO(cart, cartItems.ToList());
                    return cartDTO;
                
            
        }
        #endregion

        #region ViewCart
        /// <summary>
        /// Fetches Cart Based on User Id
        /// </summary>
        /// <returns>CartDTO</returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoItemsInCartException"></exception>

        public async Task<CartDTO> ViewCart()
        {
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var cart = await _cartRepository.GetByUserId((int)uid);
            if (cart == null)
            {
                throw new NoItemsInCartException();
            }
            var cartItems = await _cartItemsRepository.GetCartItemsWithCartId(cart.Id);
            if (cartItems == null || cartItems.ToList().Count == 0)
            {
                throw new NoItemsInCartException();
            }
            var cartDTO = _dTOService.MapCartToCartDTO(cart, cartItems.ToList());
            return cartDTO;
        }
        #endregion

        #region EditCart
        /// <summary>
        /// Edit CartItems Qunatity
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <param name="newQty"></param>
        /// <returns>CartDTO</returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoItemsInCartException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="DbException"></exception>
        public async Task<CartDTO> EditCart(int cartItemId, int newQty)
        {
                var uid = _tokenService.GetUidFromToken();
                if (uid == null)
                {
                    throw new UnauthorizedUserException("Token Invalid");
                }
                var cart = await _cartRepository.GetByUserId((int)uid);
                if (cart == null) throw new NoItemsInCartException();
               
                var cartItem = await _cartItemsRepository.GetById(cartItemId);
                if (cartItem == null) throw new NoItemsInCartException();
                if (cartItem.CartId != cart.Id)
                {
                    throw new UnauthorizedAccessException("You Dont Acess To this cartItem");
                }
            var stock = await _videoRepository.GetStockOfVideo(cartItem.VideoId);
                if (newQty > stock )
                {
                    throw new QunatityOutOfStockException("We dont Have Stock");
                }
            float price = await _videoRepository.GetPriceOfVideo(cartItem.VideoId);
             
                cart.TotalPrice -= cartItem.Price;
                cartItem.Qty = newQty;
                cartItem.Price = price * newQty;
                cart.TotalPrice += cartItem.Price;
                cart = await _cartRepository.Update(cart);
                cartItem = await _cartItemsRepository.Update(cartItem);
                if(cart == null || cartItem == null) { throw new DbException(); }
                var cartItems = await _cartItemsRepository.GetCartItemsWithCartId(cart.Id);
                var cartDTO = _dTOService.MapCartToCartDTO(cart, cartItems.ToList());
                return cartDTO;
 
        }
        #endregion

        #region DeleteCartItem
        /// <summary>
        /// Delete CartItem
        /// </summary>
        /// <param name="cartItemId"></param>
        /// <returns>CartDTO</returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoItemsInCartException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="DbException"></exception>
        public async Task<CartDTO> DeleteCartItem(int cartItemId)
        {
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var cart = await _cartRepository.GetByUserId((int)uid);
            if (cart == null) throw new NoItemsInCartException();
            var cartItem = await _cartItemsRepository.GetById(cartItemId);
            if (cartItem == null) throw new NoItemsInCartException();
            if(cartItem.CartId != cart.Id) throw new UnauthorizedAccessException("You Dont Acess To this cartItem");
            cart.TotalPrice -= cartItem.Price;
            
            var deleteCart = await _cartItemsRepository.Delete(cartItemId);
            if (deleteCart == false) throw new DbException();
            await _cartRepository.Update(cart);
            var cartItems = await _cartItemsRepository.GetCartItemsWithCartId(cart.Id);
            return _dTOService.MapCartToCartDTO(cart, cartItems.ToList());
        }
        #endregion

        #region CartItemsCount
        public async Task<int> CartItemsCount()
        {
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var count = await _cartRepository.CartItemsCount((int)uid);
            if (count == -1) throw new NoSuchItemInDbException("No Such Cart Found");
            return count;

        }
        #endregion

    }
}
