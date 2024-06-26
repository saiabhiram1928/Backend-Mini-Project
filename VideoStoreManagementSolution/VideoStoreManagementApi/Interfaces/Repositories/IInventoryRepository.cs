﻿using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface IInventoryRepository : IRepository<int, Inventory>
    {
        public Task<int> GetQty(int id);
        public Task<bool> UpdateStock(int qty, int videoId);
    }
}
