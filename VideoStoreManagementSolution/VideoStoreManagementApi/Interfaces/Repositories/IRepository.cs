namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface IRepository<K, T> where T : class
    {
        public Task<T> Add(T item);
        public Task<bool> Delete(K key);
        public Task<T> Update(T item);
        public Task<T> GetById(K key);
        public Task<IEnumerable<T>> GetAll();
    }
}
