using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Repositories;
using VideoStoreManagementApi.Services;
using VideoStoreManagementApi.Services.Helpers;

namespace VideoStoreManagementTest.ServiceTest
{
    internal class CartServiceTest
    {
        
        private CartService _cartService;
        private Video _testVideo;
        private Cart _testCart;
        private VideoStoreContext _context;
        private Mock<ICartRepository> _cartRepositoryMock;
        private Mock<ICartItemsRepository> _cartItemsRepositoryMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<IVideoRepository> _videoRepositoryMock;
        private Mock<IDTOService> _dTOServiceMock;
        private Mock<IInventoryRepository> _inventoryRepositoryMock;

        private CartHelpers _cartHelpers;
        private CartItem _testCartItem;

       

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(optionsBuilder.Options);
            // Initialize mocks
            _cartRepositoryMock = new Mock<ICartRepository>();
            _cartItemsRepositoryMock = new Mock<ICartItemsRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _videoRepositoryMock = new Mock<IVideoRepository>();
            _dTOServiceMock = new Mock<IDTOService>();
            _inventoryRepositoryMock = new Mock<IInventoryRepository>();

            _cartHelpers = new CartHelpers();

            // Mock token service to return a test user ID
            _tokenServiceMock.Setup(ts => ts.GetUidFromToken()).Returns(1);

            // Initialize CartService with mocks
            _cartService = new CartService(
                _cartRepositoryMock.Object,
                _cartItemsRepositoryMock.Object,
                _tokenServiceMock.Object,
                _videoRepositoryMock.Object,
                _dTOServiceMock.Object,
                _inventoryRepositoryMock.Object
            );

            // Setup test video
            _testVideo = new Video
            {
                Id = 1,
                Genre = VideoStoreManagementApi.Models.Enums.Genre.Action,
                Director = "Test Director",
                Price = 10.22f,
                ReleaseDate = DateTime.Now.AddDays(-2),
                Description = "Test Description",
                Tittle = "Test Title",
            };
            // Setup test cart item
            _testCartItem = new CartItem
            {
                Id = 1,
                CartId = 1,
                VideoId = 1,
                Qty = 1,
                Price = 10.22f,

            };

            // Setup test cart
            _testCart = new Cart
            {
                Id = 1,
                CustomerId = 1,
                TotalPrice = 10.22f,
               
            };

           

            // Mock repository methods
            _videoRepositoryMock.Setup(vr => vr.CheckVideoExistById(It.IsAny<int>())).ReturnsAsync(true);
            _videoRepositoryMock.Setup(vr => vr.GetPriceOfVideo(It.IsAny<int>())).ReturnsAsync(_testVideo.Price);
            _inventoryRepositoryMock.Setup(ir => ir.GetQty(It.IsAny<int>())).ReturnsAsync(10);

            _cartRepositoryMock.Setup(cr => cr.GetByUserId(It.IsAny<int>())).ReturnsAsync(_testCart);
            _cartRepositoryMock.Setup(cr => cr.Add(It.IsAny<Cart>())).ReturnsAsync(_testCart);
            _cartRepositoryMock.Setup(cr => cr.Update(It.IsAny<Cart>())).ReturnsAsync(_testCart);

            _cartItemsRepositoryMock.Setup(ci => ci.GetCartItemWithVideoId(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_testCartItem);
            _cartItemsRepositoryMock.Setup(ci => ci.Add(It.IsAny<CartItem>())).ReturnsAsync(_testCartItem);
            _cartItemsRepositoryMock.Setup(ci => ci.Update(It.IsAny<CartItem>())).ReturnsAsync(_testCartItem);
            _cartItemsRepositoryMock.Setup(ci => ci.GetCartItemsWithCartId(It.IsAny<int>())).ReturnsAsync(new List<CartItem> { _testCartItem });
            _dTOServiceMock.Setup(d => d.MapCartToCartDTO(It.IsAny<Cart>(), It.IsAny<List<CartItem>>()))
                .Returns(new CartDTO { cartId = _testCart.Id, TotalPrice = 30, customerId = 1 ,
                    CartItems = new List<CartItemDTO> {
                        new CartItemDTO { CartItemId = 1, VideoId = _testVideo.Id, Qty = 2, Price = 2 * _testVideo.Price, VideoTittle = _testVideo.Tittle }
                    }
                });
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
       public async Task AddToCartPassTest()
        {
            var result = await _cartService.AddToCart(1, 1);

           
            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.CartItems[0].Qty, Is.EqualTo(2));
           
        }
        [Test]
        public void AddToCartFailTest_VideoDoesntExist()
        {
            //Arrange
            int videoId = 2, qty = 3;
            _videoRepositoryMock.Setup(vr => vr.CheckVideoExistById(It.IsAny<int>())).ReturnsAsync(false);
            //Act && Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(async () => await _cartService.AddToCart(videoId, qty));
        }
        [Test]
        public void AddToCartFailTest_ExceedStock()
        {
            //Arrange
            int videoId = 1, qty = 11;
            //Act && Assert
            Assert.ThrowsAsync<QunatityOutOfStockException>(() => _cartService.AddToCart(videoId, qty));
        }
        [Test]
        public async Task ViewCartPassTest()
        {
            var res = await _cartService.ViewCart();
            Assert.IsNotNull(res);
            Assert.That(res.customerId, Is.EqualTo(1));
            Assert.IsNotNull(res.CartItems);
        }
        [Test]
        public void ViewCartFailTest_InvalidToken()
        {
            //Arragne 
            _tokenServiceMock.Setup(ts => ts.GetUidFromToken()).Returns((int?)null);
            //Act && Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _cartService.ViewCart() );
        }
        [Test]
        public void ViewCartFailTest_NoItemsInCart()
        {
            _cartRepositoryMock.Setup(cr => cr.GetByUserId(It.IsAny<int>())).ReturnsAsync((Cart)null);

            Assert.ThrowsAsync<NoItemsInCartException>(async () => await _cartService.ViewCart());
        }
        [Test]
        public void ViewCartFailTest_NoItemsInCartItems()
        {
            _cartItemsRepositoryMock.Setup(ci => ci.GetCartItemsWithCartId(It.IsAny<int>())).ReturnsAsync(new List<CartItem> {});
            Assert.ThrowsAsync<NoItemsInCartException>(async () => await _cartService.ViewCart());
        }
        [Test]
        public async Task EditCartPassTest()
        {
            // Arrange
            int cartItemId = 1, newQty = 3;
            float price = 10.22f;
            _cartItemsRepositoryMock.Setup(ci => ci.GetById(cartItemId)).ReturnsAsync(_testCartItem);
            _videoRepositoryMock.Setup(vr => vr.GetPriceOfVideo(It.IsAny<int>())).ReturnsAsync(price);
            _dTOServiceMock.Setup(d => d.MapCartToCartDTO(It.IsAny<Cart>(), It.IsAny<List<CartItem>>()))
                .Returns(new CartDTO
                {
                    cartId = _testCart.Id,
                    TotalPrice = 30,
                    customerId = 1,
                    CartItems = new List<CartItemDTO> {
                        new CartItemDTO { CartItemId = 1, VideoId = _testVideo.Id, Qty = newQty, Price = 2 * _testVideo.Price, VideoTittle = _testVideo.Tittle }
                    }
                });
            // Act
            var result = await _cartService.EditCart(cartItemId, newQty);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.CartItems[0].Qty, Is.EqualTo(newQty));
       
        }
        [Test]
        public void EditCartFailTest_InvalidToken()
        {
            // Arrange
            _tokenServiceMock.Setup(ts => ts.GetUidFromToken()).Returns((int?)null);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _cartService.EditCart(1, 2));
        }

        [Test]
        public void EditCartFailTest_NoItemsInCart()
        {
            // Arrange
            _cartRepositoryMock.Setup(cr => cr.GetByUserId(It.IsAny<int>())).ReturnsAsync((Cart)null);

            // Act & Assert
            Assert.ThrowsAsync<NoItemsInCartException>(async () => await _cartService.EditCart(1, 2));
        }

        [Test]
        public void EditCartFailTest_CartItemNotFound()
        {
            // Arrange
            _cartItemsRepositoryMock.Setup(ci => ci.GetById(It.IsAny<int>())).ReturnsAsync((CartItem)null);

            // Act & Assert
            Assert.ThrowsAsync<NoItemsInCartException>(async () => await _cartService.EditCart(1, 2));
        }

        [Test]
        public void EditCartFailTest_UnauthorizedAccess()
        {
            // Arrange
            var otherCartItem = new CartItem { Id = 2, CartId = 2, VideoId = 1, Qty = 1, Price = 10.22f };
            _cartItemsRepositoryMock.Setup(ci => ci.GetById(It.IsAny<int>())).ReturnsAsync(otherCartItem);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _cartService.EditCart(2, 2));
        }

        [Test]
        public async Task DeleteCartItemPassTest()
        {
            // Arrange
            int cartItemId = 1;
            _cartItemsRepositoryMock.Setup(ci => ci.GetById(cartItemId)).ReturnsAsync(_testCartItem);
            _cartItemsRepositoryMock.Setup(ci => ci.Delete(cartItemId)).ReturnsAsync(true);
            _dTOServiceMock.Setup(d => d.MapCartToCartDTO(It.IsAny<Cart>(), It.IsAny<List<CartItem>>()))
               .Returns(new CartDTO
               {
                   cartId = _testCart.Id,
                   TotalPrice = 0,
                   customerId = 1,
                   CartItems = new List<CartItemDTO> {
                        
                   }
               });

            // Act
            var result = await _cartService.DeleteCartItem(cartItemId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.TotalPrice, Is.EqualTo(0));
            Assert.IsEmpty(result.CartItems);
        }

        [Test]
        public void DeleteCartItemFailTest_InvalidToken()
        {
            // Arrange
            _tokenServiceMock.Setup(ts => ts.GetUidFromToken()).Returns((int?)null);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedUserException>(async () => await _cartService.DeleteCartItem(1));
        }

        [Test]
        public void DeleteCartItemFailTest_NoItemsInCart()
        {
            // Arrange
            _cartRepositoryMock.Setup(cr => cr.GetByUserId(It.IsAny<int>())).ReturnsAsync((Cart)null);

            // Act & Assert
            Assert.ThrowsAsync<NoItemsInCartException>(async () => await _cartService.DeleteCartItem(1));
        }

        [Test]
        public void DeleteCartItemFailTest_CartItemNotFound()
        {
            // Arrange
            _cartItemsRepositoryMock.Setup(ci => ci.GetById(It.IsAny<int>())).ReturnsAsync((CartItem)null);

            // Act & Assert
            Assert.ThrowsAsync<NoItemsInCartException>(async () => await _cartService.DeleteCartItem(1));
        }

        [Test]
        public void DeleteCartItemFailTest_UnauthorizedAccess()
        {
            // Arrange
            var otherCartItem = new CartItem { Id = 2, CartId = 2, VideoId = 1, Qty = 1, Price = 10.22f };
            _cartItemsRepositoryMock.Setup(ci => ci.GetById(It.IsAny<int>())).ReturnsAsync(otherCartItem);

            // Act & Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _cartService.DeleteCartItem(2));
        }


    }
}
