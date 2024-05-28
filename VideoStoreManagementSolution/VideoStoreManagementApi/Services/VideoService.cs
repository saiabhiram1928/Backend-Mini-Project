using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Repositories;

namespace VideoStoreManagementApi.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IDTOService _dtoService;
        private readonly IInventoryRepository _inventoryRepository;

        public VideoService(IVideoRepository videoRepository , IDTOService dTOService, IInventoryRepository inventoryRepository)
        {
            _videoRepository = videoRepository;
            _dtoService = dTOService;
            _inventoryRepository = inventoryRepository;
        }
        public async Task<List<Video>> GetAllVideos()
        {
            var videoList = await _videoRepository.GetAll();
            if(videoList == null) { throw new NoItemsInDbException("No Videos Presnet"); }
            return videoList.ToList();
        }
        public async Task<Video> GetVideoById(int id)
        {
            var video = await _videoRepository.GetById(id);
            if(video == null)
            {
                throw new NoSuchItemInDbException("The Video With Given Id Doesnt Exist");
            }
            return video;
        }
        public async Task<IList<Video>> Search(string name)
        {
            var list = await _videoRepository.QueryContains(name);
            return list;
        }
        public async Task<Video> AddVideo(VideoRegisterDTO videoRegisterDTO)
        {
            var videoExist = await _videoRepository.GetVideoByTittle(videoRegisterDTO.Tittle);
            if (videoExist)
            {
                throw new DuplicateItemException("The Video With Given Tittle Already Exist");
            }

            var video = _dtoService.MapVideoRegisterDTOToVideo(videoRegisterDTO);
            video = await _videoRepository.Add(video);
            if(video == null)
            {
                throw new DbException();
            }
            Inventory inventory = new Inventory();
            inventory.VideoId = video.Id;
            inventory.Stock = videoRegisterDTO.Stock;
            inventory.LastUpdate = DateTime.Now;
            inventory = await _inventoryRepository.Add(inventory);
            if(inventory == null)
            {
                await _videoRepository.Delete(video.Id);
                throw new DbException();
            }
            return video;
        }
        
    }
}
