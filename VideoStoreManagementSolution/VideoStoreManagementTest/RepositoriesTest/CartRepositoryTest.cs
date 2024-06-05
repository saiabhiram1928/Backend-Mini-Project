using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Repositories;

namespace VideoStoreManagementTest.RepositoriesTest
{
    internal class CartRepositoryTest
    {
        private VideoStoreContext _context;
        private ICartRepository _cartRepository;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(optionsBuilder.Options);
            _cartRepository = new CartRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddCart_PassTest()
        {
            var cart = new Cart
            {
                CustomerId = 1,
                TotalPrice = 100.0f,
                CartItems = new List<CartItem>()
            };

            var addedCart = await _cartRepository.Add(cart);

            Assert.IsNotNull(addedCart);
            Assert.That(addedCart.TotalPrice, Is.EqualTo(cart.TotalPrice));
        }

        [Test]
        public async Task AddCart_FailTest()
        {
            var cart = new Cart
            {
                CustomerId = 1,
                TotalPrice = 100.0f,
                CartItems = new List<CartItem>()
            };
            await _cartRepository.Add(cart);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _cartRepository.Add(cart);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task UpdateCart_PassTest()
        {
            var cart = new Cart
            {
                CustomerId = 1,
                TotalPrice = 100.0f,
                CartItems = new List<CartItem>()
            };
            cart = await _cartRepository.Add(cart);

            cart.TotalPrice = 80.0f;
            var updatedCart = await _cartRepository.Update(cart);

            Assert.IsNotNull(updatedCart);
            Assert.That(updatedCart.TotalPrice, Is.EqualTo(80.0f));
        }

        [Test]
        public async Task UpdateCart_FailTest()
        {
            var cart = new Cart
            {
                CustomerId = 1,
                TotalPrice = 100.0f,
                CartItems = new List<CartItem>()
            };
            cart = await _cartRepository.Add(cart);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                cart.Id = 100;
                await _cartRepository.Update(cart);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task DeleteCart_PassTest()
        {
            var cart = new Cart
            {
                CustomerId = 1,
                TotalPrice = 100.0f,
                CartItems = new List<CartItem>()
            };
            cart = await _cartRepository.Add(cart);

            var result = await _cartRepository.Delete(cart.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteCart_FailTest()
        {
            var result = await _cartRepository.Delete(100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetCartById_PassTest()
        {
            var cart = new Cart
            {
                CustomerId = 1,
                TotalPrice = 100.0f,
                CartItems = new List<CartItem>()
            };
            cart = await _cartRepository.Add(cart);

            var result = await _cartRepository.GetById(cart.Id);

            Assert.IsNotNull(result);
            Assert.That(result.TotalPrice, Is.EqualTo(cart.TotalPrice));
        }

        [Test]
        public async Task GetCartById_FailTest()
        {
            var result = await _cartRepository.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllCarts_PassTest()
        {
            var carts = new List<Cart>
            {
                new Cart
                {
                    CustomerId = 1,
                    TotalPrice = 100.0f,
                    CartItems = new List<CartItem>()
                },
                new Cart
                {
                    CustomerId = 2,
                    TotalPrice = 50.0f,
                    CartItems = new List<CartItem>()
                }
            };
            foreach (var cart in carts)
            {
                await _cartRepository.Add(cart);
            }

            var result = await _cartRepository.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(carts, result);
        }

        [Test]
        public async Task GetAllCarts_FailTest()
        {
            var result = await _cartRepository.GetAll();

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task CheckCartExist_PassTest()
        {
            var cart = new Cart
            {
                CustomerId = 1,
                TotalPrice = 100.0f,
                CartItems = new List<CartItem>()
            };
            await _cartRepository.Add(cart);

            var result = await _cartRepository.CheckCartExist(cart.CustomerId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task CheckCartExist_FailTest()
        {
            var result = await _cartRepository.CheckCartExist(100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetCartByUserId_PassTest()
        {
            var cart = new Cart
            {
                CustomerId = 1,
                TotalPrice = 100.0f,
                CartItems = new List<CartItem>()
            };
            await _cartRepository.Add(cart);

            var result = await _cartRepository.GetByUserId(cart.CustomerId);

            Assert.IsNotNull(result);
            Assert.That(result.TotalPrice, Is.EqualTo(cart.TotalPrice));
        }

        [Test]
        public async Task GetCartByUserId_FailTest()
        {
            var result = await _cartRepository.GetByUserId(100);

            Assert.IsNull(result);
        }
    }
}
