using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Models.Enums;
using VideoStoreManagementApi.Repositories;

namespace VideoStoreManagementApi.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IDTOService _dtoService;
        private readonly IInventoryRepository _inventoryRepository;

        #region Constructor

        public VideoService(IVideoRepository videoRepository , IDTOService dTOService, IInventoryRepository inventoryRepository)
        {
            _videoRepository = videoRepository;
            _dtoService = dTOService;
            _inventoryRepository = inventoryRepository;
        }
        #endregion

        #region GetAllVideos
        /// <summary>
        /// Fetches All Videos With Pagination
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>VideoDTO</returns>
        /// <exception cref="NoItemsInDbException"></exception>
        public async Task<List<VideoDTO>> GetAllVideos(int pageNumber, int pageSize)
        {
            var videoList = await _videoRepository.GetAllByPagination(pageNumber, pageSize);
            if(videoList == null) { throw new NoItemsInDbException("No Videos Presnet"); }
            List<VideoDTO> videoDTOs = new List<VideoDTO>();
            foreach(var video in videoList)
            {
                int qty = await _inventoryRepository.GetQty(video.Id);
                videoDTOs.Add(_dtoService.MapVideoToVideoDTO(video, qty));
            }
            return videoDTOs;
        }
        #endregion

        #region GetVideoById
        /// <summary>
        /// Fetches a Video by VideoId
        /// </summary>
        /// <param name="id">VideoId</param>
        /// <returns>VideoDTO</returns>
        /// <exception cref="NoSuchItemInDbException"></exception>
        public async Task<VideoDTO> GetVideoById(int id)
        {
            var video = await _videoRepository.GetById(id);
            if(video == null)
            {
                throw new NoSuchItemInDbException("The Video With Given Id Doesnt Exist");
            }
            int stock = await _inventoryRepository.GetQty(id);
            return _dtoService.MapVideoToVideoDTO(video, stock );
        }
        #endregion

        #region Search
        /// <summary>
        /// Query a Video Based on Given Key Text
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns> Returns a List of Videos</returns>
        public async Task<IList<VideoDTO>> Search(string name , int pageNumber, int pageSize)
        {
            var list = await _videoRepository.QueryContains(name,pageNumber, pageSize);
            List<VideoDTO> videoDTOs = new List<VideoDTO>();
            foreach (var  item in list)
            {
                int qty = await _inventoryRepository.GetQty(item.Id);
                videoDTOs.Add(_dtoService.MapVideoToVideoDTO(item, qty));
            }
            return videoDTOs;
        }
        #endregion

        #region AddVideo
        /// <summary>
        /// Add Videos to the database Admin Feature
        /// </summary>
        /// <param name="videoRegisterDTO"></param>
        /// <returns>VideoDTO</returns>
        /// <exception cref="DuplicateItemException"></exception>
        /// <exception cref="DbException"></exception>
        public async Task<VideoDTO> AddVideo(VideoRegisterDTO videoRegisterDTO)
        {
            var videoExist = await _videoRepository.CheckVideoExistByTittle(videoRegisterDTO.Tittle);
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
            return _dtoService.MapVideoToVideoDTO(video , inventory.Stock);
        }
        #endregion

        #region UpdateVideoDetails
        /// <summary>
        /// Updates Movie Details Admin Fetaure
        /// </summary>
        /// <param name="videoRegisterDTO"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="DbException"></exception>
        public async Task<VideoDTO> UpdateVideoDetails(VideoRegisterDTO videoRegisterDTO , int id)
        {
            var videoExist = await _videoRepository.CheckVideoExistById(id);
            if (!videoExist) 
            {
                throw new NoSuchItemInDbException("The Video With Given Id Doesnt Exist");
                
            }
            var video = _dtoService.MapVideoRegisterDTOToVideo(videoRegisterDTO);
            video.Id = id;
            var videoReturn = await _videoRepository.Update(video);
            if (videoReturn == null)
            {
                throw new DbException();
            }
            int stock = await _inventoryRepository.GetQty(videoReturn.Id);
            return _dtoService.MapVideoToVideoDTO(videoReturn, stock);

        }
        #endregion

        #region GetVideosByGenre
        /// <summary>
        /// Fetches Video based on genre
        /// </summary>
        /// <param name="genre"></param>
        /// <returns>VideoDTO</returns>
        /// <exception cref="NoItemsInDbException"></exception>

        public async Task<IList<VideoDTO>> GetVideosByGenre(Genre genre)
        {
            var videos = await _videoRepository.GetAll();
            if(videos == null)
            {
                throw new NoItemsInDbException("No Items Present in database");
            }
            videos = videos.Where(v => v.Genre == genre).OrderBy(v => v.Id).ToList() ;
            List<VideoDTO> videoDTOs = new List<VideoDTO>();
            foreach (var video in videos)
            {
                int qty = await _inventoryRepository.GetQty(video.Id);
                videoDTOs.Add(_dtoService.MapVideoToVideoDTO(video, qty));
            }
            return videoDTOs;
        }
        #endregion
    }
}
