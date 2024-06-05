using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Repositories;

namespace VideoStoreManagementTest.RepositoriesTest
{
    internal class CartItemRepositoryTest
    {
        private VideoStoreContext _context;
        private CartItemRepository _cartItemRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test")
                .Options;

            _context = new VideoStoreContext(options);
            _cartItemRepository = new CartItemRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddCartItem_PassTest()
        {
            var cartItem = new CartItem
            {
                CartId = 1,
                VideoId = 1,
                Qty = 1,
                Price = 10.0f
            };

            var addedCartItem = await _cartItemRepository.Add(cartItem);

            Assert.IsNotNull(addedCartItem);
            Assert.That(addedCartItem.Price, Is.EqualTo(cartItem.Price));
        }

        [Test]
        public async Task AddCartItem_FailTest()
        {
            var cartItem = new CartItem
            {
                CartId = 1,
                VideoId = 1,
                Qty = 1,
                Price = 10.0f
            };
            await _cartItemRepository.Add(cartItem);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _cartItemRepository.Add(cartItem);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task UpdateCartItem_PassTest()
        {
            var cartItem = new CartItem
            {
                CartId = 1,
                VideoId = 1,
                Qty = 1,
                Price = 10.0f
            };
            cartItem = await _cartItemRepository.Add(cartItem);

            cartItem.Price = 15.0f;
            var updatedCartItem = await _cartItemRepository.Update(cartItem);

            Assert.IsNotNull(updatedCartItem);
            Assert.That(updatedCartItem.Price, Is.EqualTo(15.0f));
        }

        [Test]
        public async Task UpdateCartItem_FailTest()
        {
            var cartItem = new CartItem
            {
                CartId = 1,
                VideoId = 1,
                Qty = 1,
                Price = 10.0f
            };
            cartItem = await _cartItemRepository.Add(cartItem);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                cartItem.Id = 100;
                await _cartItemRepository.Update(cartItem);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task DeleteCartItem_PassTest()
        {
            var cartItem = new CartItem
            {
                CartId = 1,
                VideoId = 1,
                Qty = 1,
                Price = 10.0f
            };
            cartItem = await _cartItemRepository.Add(cartItem);

            var result = await _cartItemRepository.Delete(cartItem.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteCartItem_FailTest()
        {
            var result = await _cartItemRepository.Delete(100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetCartItemById_PassTest()
        {
            var cartItem = new CartItem
            {
                CartId = 1,
                VideoId = 1,
                Qty = 1,
                Price = 10.0f
            };
            cartItem = await _cartItemRepository.Add(cartItem);

            var result = await _cartItemRepository.GetById(cartItem.Id);

            Assert.IsNotNull(result);
            Assert.That(result.Price, Is.EqualTo(cartItem.Price));
        }

        [Test]
        public async Task GetCartItemById_FailTest()
        {
            var result = await _cartItemRepository.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllCartItems_PassTest()
        {
            var video = new Video
            {
                Id = 1,
                Tittle = "Sample Video",
                Director = "test director",
                ReleaseDate = DateTime.Now,
                Genre = VideoStoreManagementApi.Models.Enums.Genre.Action,
                Description = "Description",
                Price = 10.0f
            };
            await _context.Videos.AddAsync(video);
            var cart = new Cart
            {
                Id = 1,
                CustomerId = 1,
                TotalPrice = 0.0f
            };
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            var cartItems = new List<CartItem>
            {

                new CartItem
                {
                    CartId = cart.Id,
                    VideoId = video.Id,
                    Qty = 1,
                    Price = 10.0f
                },
               
            };
           
               await _cartItemRepository.Add(cartItems[0]);
           
            

            var result = await _cartItemRepository.GetAll();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count()  == 1);
        }

        [Test]
        public async Task GetAllCartItems_FailTest()
        {
            var result = await _cartItemRepository.GetAll();

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task CheckCartItemExist_PassTest()
        {
            var cartItem = new CartItem
            {
                CartId = 1,
                VideoId = 1,
                Qty = 1,
                Price = 10.0f
            };
            await _cartItemRepository.Add(cartItem);

            var result = await _cartItemRepository.CheckCartItemExist(cartItem.VideoId, cartItem.CartId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CheckCartItemExist_FailTest()
        {
            var result = await _cartItemRepository.CheckCartItemExist(1, 100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetCartItemWithVideoId_PassTest()
        {
            var video = new Video
            {
                Id = 1,
                Tittle = "Sample Video",
                Director = "test director",
                ReleaseDate = DateTime.Now,
                Genre = VideoStoreManagementApi.Models.Enums.Genre.Action,
                Description = "Description",
                Price = 10.0f
            };
            await _context.Videos.AddAsync(video);
            var cart = new Cart
            {
                Id = 1,
                CustomerId = 1,
                TotalPrice = 0.0f
            };
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            var cartItem = new CartItem
            {
                CartId = cart.Id,
                VideoId = video.Id,
                Qty = 1,
                Price = 10.0f
            };
            await _cartItemRepository.Add(cartItem);

            var result = await _cartItemRepository.GetCartItemWithVideoId(cartItem.VideoId, cartItem.CartId);

            Assert.IsNotNull(result);
            Assert.That(result.Price, Is.EqualTo(cartItem.Price));
        }

        [Test]
        public async Task GetCartItemWithVideoId_FailTest()
        {
            var result = await _cartItemRepository.GetCartItemWithVideoId(1, 100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetCartItemsWithCartId_PassTest()
        {
            var video = new Video
            {
           
                Tittle = "Sample Video",
                Director = "Test",
                Description = "Description",
                Price = 10.0f
            };
          await _context.Videos.AddAsync(video);
            //var cart = new Cart
            //{
            //    Id = 1,
            //    CustomerId = 1,
            //    TotalPrice = 10.0f
            //};
            //await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            var cartItems = new List<CartItem>
            {
                new CartItem
                {
                    CartId = 1,
                    VideoId =video.Id,
                    Qty = 1,
                    Price = 10.0f
                },
              
            };
            foreach (var cartItem in cartItems)
            {
                await _cartItemRepository.Add(cartItem);
            }

            var result = await _cartItemRepository.GetCartItemsWithCartId(1);

            Assert.IsNotNull(result);
            Assert.True(result.ToList().Count == 1);
            
        }

        [Test]
        public async Task GetCartItemsWithCartId_FailTest()
        {
            var result = await _cartItemRepository.GetCartItemsWithCartId(100);

            Assert.IsEmpty(result);
        }
    }
}
