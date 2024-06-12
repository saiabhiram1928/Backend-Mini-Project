using System.Data.Common;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces;

namespace VideoStoreManagementApi.Repositories
{
    public abstract class CRUDRepository<K, T> where T : class
    {
        protected readonly VideoStoreContext _context;

        public CRUDRepository(VideoStoreContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T item)
        {
            if (item == null) return null;
            
            try
            {

                _context.Add(item);
               await _context.SaveChangesAsync();
                return item;
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"Database error: {dbEx.Message}");
               
            }
            return null;
            
        }

        public async Task<bool> Delete(int key)
        {
            try
            {
                var item = await GetById(key);
                if (item == null) return false;

                _context.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"Database error: {dbEx.Message}");
            }

            return false;
               
        }

        public abstract Task<T> GetById(int key);

        public abstract Task<IEnumerable<T>> GetAll();
        public async Task<T> Update(T item)
        {
            try
            {
                _context.Update(item);
               await _context.SaveChangesAsync();
                return item;
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"Database error: {dbEx.Message}");
               
            }
            return null;
        }
    }
}
