using VideoStoreManagementApi.Models;

namespace VideoStoreManagementApi.Interfaces.Repositories
{
    public interface IVideoRepository : IRepository<int, Video>
    {
        public Task<IList<Video>> QueryContains(string text, int pageNumber, int pageSize);
        public Task<bool> CheckVideoExistByTittle(string text);
        public Task<bool> CheckVideoExistById(int id);
        public Task<IEnumerable<Video>> GetAllByPagination(int pageNumber, int pageSize);
        public Task<float> GetPriceOfVideo(int id);
    }
}
