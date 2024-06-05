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
    internal class InventoryRepositoryTest
    {

        private VideoStoreContext _context;
        private IInventoryRepository _inventoryRepository;

        [SetUp]
        public void SetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>()
                .UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(optionsBuilder.Options);
            _inventoryRepository = new InventoryRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddInventory_PassTest()
        {
            var inventory = new Inventory
            {
                VideoId = 1,
                Stock = 100,
                LastUpdate = DateTime.Now
            };

            var addedInventory = await _inventoryRepository.Add(inventory);

            Assert.IsNotNull(addedInventory);
            Assert.That(addedInventory.Stock, Is.EqualTo(inventory.Stock));
        }

        [Test]
        public async Task AddInventory_FailTest()
        {
            var inventory = new Inventory
            {
                VideoId = 1,
                Stock = 100,
                LastUpdate = DateTime.Now
            };
            await _inventoryRepository.Add(inventory);

            var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await _inventoryRepository.Add(inventory);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task UpdateInventory_PassTest()
        {
            var inventory = new Inventory
            {
                VideoId = 1,
                Stock = 100,
                LastUpdate = DateTime.Now
            };
            inventory = await _inventoryRepository.Add(inventory);

            inventory.Stock = 80;
            var updatedInventory = await _inventoryRepository.Update(inventory);

            Assert.IsNotNull(updatedInventory);
            Assert.That(updatedInventory.Stock, Is.EqualTo(80));
        }

        [Test]
        public async Task UpdateInventory_FailTest()
        {
            var inventory = new Inventory
            {
                VideoId = 1,
                Stock = 100,
                LastUpdate = DateTime.Now
            };
            inventory = await _inventoryRepository.Add(inventory);

            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                inventory.VideoId = 100;
                await _inventoryRepository.Update(inventory);
            });
            Assert.IsNotNull(ex);
        }

        [Test]
        public async Task DeleteInventory_PassTest()
        {
            var inventory = new Inventory
            {
                VideoId = 1,
                Stock = 100,
                LastUpdate = DateTime.Now
            };
            inventory = await _inventoryRepository.Add(inventory);

            var result = await _inventoryRepository.Delete(inventory.VideoId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteInventory_FailTest()
        {
            var result = await _inventoryRepository.Delete(100);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetInventoryById_PassTest()
        {
            var inventory = new Inventory
            {
                VideoId = 1,
                Stock = 100,
                LastUpdate = DateTime.Now
            };
            inventory = await _inventoryRepository.Add(inventory);

            var result = await _inventoryRepository.GetById(inventory.VideoId);

            Assert.IsNotNull(result);
            Assert.That(result.Stock, Is.EqualTo(inventory.Stock));
        }

        [Test]
        public async Task GetInventoryById_FailTest()
        {
            var result = await _inventoryRepository.GetById(100);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllInventories_PassTest()
        {
            var inventories = new List<Inventory>
            {
                new Inventory
                {
                    VideoId = 1,
                    Stock = 100,
                    LastUpdate = DateTime.Now
                },
                new Inventory
                {
                    VideoId = 2,
                    Stock = 50,
                    LastUpdate = DateTime.Now
                }
            };
            foreach (var inventory in inventories)
            {
                await _inventoryRepository.Add(inventory);
            }

            var result = await _inventoryRepository.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(inventories, result);
        }

        [Test]
        public async Task GetAllInventories_FailTest()
        {
            var result = await _inventoryRepository.GetAll();

            Assert.IsEmpty(result);
        }

        [Test]
        public async Task GetQty_PassTest()
        {
            var inventory = new Inventory
            {
                VideoId = 1,
                Stock = 100,
                LastUpdate = DateTime.Now
            };
            await _inventoryRepository.Add(inventory);

            var result = await _inventoryRepository.GetQty(inventory.VideoId);

            Assert.That(result, Is.EqualTo(100));
        }

        [Test]
        public async Task GetQty_FailTest()
        {
            var result = await _inventoryRepository.GetQty(100);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task UpdateStock_PassTest()
        {
            var inventory = new Inventory
            {
                VideoId = 1,
                Stock = 100,
                LastUpdate = DateTime.Now
            };
            await _inventoryRepository.Add(inventory);

            var result = await _inventoryRepository.UpdateStock(10, inventory.VideoId);

            Assert.IsTrue(result);
            var updatedInventory = await _inventoryRepository.GetById(inventory.VideoId);
            Assert.That(updatedInventory.Stock, Is.EqualTo(90));
        }

        [Test]
        public async Task UpdateStock_FailTest()
        {
            var result = await _inventoryRepository.UpdateStock(10, 100);

            Assert.IsFalse(result);
        }
    }
}
