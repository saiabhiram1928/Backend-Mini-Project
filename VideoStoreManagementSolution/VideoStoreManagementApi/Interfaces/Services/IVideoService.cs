﻿using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Interfaces.Services
{
    public interface IVideoService
    {
        public Task<List<VideoDTO>> GetAllVideos(int pageNumber,int pageSize);
        public Task<VideoDTO> GetVideoById(int id);
        public Task<IList<VideoDTO>> Search(string name, int pageNumber, int pageSize);
        public Task<VideoDTO> AddVideo(VideoRegisterDTO videoRegisterDTO);
        public Task<VideoDTO> UpdateVideoDetails(VideoRegisterDTO videoRegisterDTO, int id);
        public Task<IList<VideoDTO>> GetVideosByGenre(Genre genre);

    }
}
