using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Services;
using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementTest.ServiceTest
{
    internal class VideoServiceTest
    {

        private Mock<IVideoRepository> _videoRepositoryMock;
        private Mock<IDTOService> _dtoServiceMock;
        private Mock<IInventoryRepository> _inventoryRepositoryMock;
        private VideoService _videoService;

        [SetUp]
        public void SetUp()
        {
            _videoRepositoryMock = new Mock<IVideoRepository>();
            _dtoServiceMock = new Mock<IDTOService>();
            _inventoryRepositoryMock = new Mock<IInventoryRepository>();
            _videoService = new VideoService(_videoRepositoryMock.Object, _dtoServiceMock.Object, _inventoryRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllVideosPassTest()
        {
            // Arrange
            var videos = new List<Video>
            {
                new Video { Id = 1, Tittle = "Video1" },
                new Video { Id = 2, Tittle = "Video2" }
            };
            var videoDTOs = new List<VideoDTO>
            {
                new VideoDTO { Id = 1, Tittle = "Video1", Stock = 10 },
                new VideoDTO { Id = 2, Tittle = "Video2", Stock = 5 }
            };

            _videoRepositoryMock.Setup(repo => repo.GetAllByPagination(1, 10)).ReturnsAsync(videos);
            _inventoryRepositoryMock.Setup(repo => repo.GetQty(It.IsAny<int>())).ReturnsAsync((int id) => id == 1 ? 10 : 5);
            _dtoServiceMock.Setup(service => service.MapVideoToVideoDTO(It.IsAny<Video>(), It.IsAny<int>())).Returns((Video v, int qty) => new VideoDTO { Id = v.Id, Tittle = v.Tittle, Stock = qty });

            // Act
            var result = await _videoService.GetAllVideos(1, 10);

            // Assert
            Assert.That(result.Count, Is.EqualTo(videoDTOs.Count));
            for (int i = 0; i < videoDTOs.Count; i++)
            {
                Assert.That(result[i].Id, Is.EqualTo(videoDTOs[i].Id));
                Assert.That(result[i].Tittle, Is.EqualTo(videoDTOs[i].Tittle));
                Assert.That(result[i].Stock, Is.EqualTo(videoDTOs[i].Stock));
            }
        }


        [Test]
        public void GetAllVideosFailTest_ThrowsNoItemsInDbException()
        {
            // Arrange
            _videoRepositoryMock.Setup(repo => repo.GetAllByPagination(1, 10)).ReturnsAsync((IEnumerable<Video>)null);

            // Act & Assert
            Assert.ThrowsAsync<NoItemsInDbException>(() => _videoService.GetAllVideos(1, 10));
        }

        [Test]
        public async Task GetVideoByIdPassTest()
        {
            // Arrange
            var video = new Video { Id = 1, Tittle = "Video1" };
            var videoDTO = new VideoDTO { Id = 1, Tittle = "Video1", Stock = 10 };

            _videoRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync(video);
            _inventoryRepositoryMock.Setup(repo => repo.GetQty(1)).ReturnsAsync(10);
            _dtoServiceMock.Setup(service => service.MapVideoToVideoDTO(video, 10)).Returns(videoDTO);

            // Act
            var result = await _videoService.GetVideoById(1);

            // Assert
            Assert.That(result.Id, Is.EqualTo(videoDTO.Id));
            Assert.That(result.Tittle, Is.EqualTo(videoDTO.Tittle));
            Assert.That(result.Stock, Is.EqualTo(videoDTO.Stock));
        }

        [Test]
        public void GetVideoByIdFailTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            _videoRepositoryMock.Setup(repo => repo.GetById(1)).ReturnsAsync((Video)null);

            // Act & Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(() => _videoService.GetVideoById(1));
        }
        [Test]
        public async Task SearchPassTest()
        {
            // Arrange
            var videos = new List<Video>
            {
                new Video { Id = 1, Tittle = "Video1" },
                new Video { Id = 2, Tittle = "Another Video" }
            };
            var videoDTOs = new List<VideoDTO>
            {
                new VideoDTO { Id = 1, Tittle = "Video1", Stock = 10 },
                new VideoDTO { Id = 2, Tittle = "Another Video", Stock = 5 }
            };

            _videoRepositoryMock.Setup(repo => repo.QueryContains("Video", 1, 10)).ReturnsAsync(videos);
            _inventoryRepositoryMock.Setup(repo => repo.GetQty(It.IsAny<int>())).ReturnsAsync((int id) => id == 1 ? 10 : 5);
            _dtoServiceMock.Setup(service => service.MapVideoToVideoDTO(It.IsAny<Video>(), It.IsAny<int>())).Returns((Video v, int qty) => new VideoDTO { Id = v.Id, Tittle = v.Tittle, Stock = qty });

            // Act
            var result = await _videoService.Search("Video", 1, 10);

            // Assert
            Assert.That(result.Count, Is.EqualTo(videoDTOs.Count));
            for (int i = 0; i < videoDTOs.Count; i++)
            {
                Assert.That(result[i].Id, Is.EqualTo(videoDTOs[i].Id));
                Assert.That(result[i].Tittle, Is.EqualTo(videoDTOs[i].Tittle));
                Assert.That(result[i].Stock, Is.EqualTo(videoDTOs[i].Stock));
            }
        }

        [Test]
        public async Task AddVideoPassTest_ReturnsVideoDTO()
        {
            // Arrange
            var videoRegisterDTO = new VideoRegisterDTO
            {
                Tittle = "New Video",
                Description = "Description",
                Genre = Genre.Action,
                Director = "Director",
                ReleaseDate = DateTime.Now,
                Price = 10.0f,
                Stock = 5
            };
            var video = new Video { Id = 1, Tittle = "New Video" };
            var videoDTO = new VideoDTO { Id = 1, Tittle = "New Video", Stock = 5 };

            _videoRepositoryMock.Setup(repo => repo.CheckVideoExistByTittle("New Video")).ReturnsAsync(false);
            _dtoServiceMock.Setup(service => service.MapVideoRegisterDTOToVideo(videoRegisterDTO)).Returns(video);
            _videoRepositoryMock.Setup(repo => repo.Add(video)).ReturnsAsync(video);
            _inventoryRepositoryMock.Setup(repo => repo.Add(It.IsAny<Inventory>())).ReturnsAsync(new Inventory { VideoId = 1, Stock = 5 });
            _dtoServiceMock.Setup(service => service.MapVideoToVideoDTO(video, 5)).Returns(videoDTO);

            // Act
            var result = await _videoService.AddVideo(videoRegisterDTO);

            // Assert
            Assert.That(result.Id, Is.EqualTo(videoDTO.Id));
            Assert.That(result.Tittle, Is.EqualTo(videoDTO.Tittle));
            Assert.That(result.Stock, Is.EqualTo(videoDTO.Stock));
        }

        [Test]
        public void AddVideoFailTest_ThrowsDuplicateItemException()
        {
            // Arrange
            var videoRegisterDTO = new VideoRegisterDTO
            {
                Tittle = "New Video"
            };

            _videoRepositoryMock.Setup(repo => repo.CheckVideoExistByTittle("New Video")).ReturnsAsync(true);

            // Act & Assert
            Assert.ThrowsAsync<DuplicateItemException>(() => _videoService.AddVideo(videoRegisterDTO));
        }

        [Test]
        public async Task UpdateVideoDetailsPassTest()
        {
            // Arrange
            var videoRegisterDTO = new VideoRegisterDTO
            {
                Tittle = "Updated Video",
                Description = "Updated Description",
                Genre = Genre.Action,
                Director = "Updated Director",
                ReleaseDate = DateTime.Now,
                Price = 15.0f,
                Stock = 10
            };
            var video = new Video { Id = 1, Tittle = "Updated Video" };
            var videoDTO = new VideoDTO { Id = 1, Tittle = "Updated Video", Stock = 10 };

            _videoRepositoryMock.Setup(repo => repo.CheckVideoExistById(1)).ReturnsAsync(true);
            _dtoServiceMock.Setup(service => service.MapVideoRegisterDTOToVideo(videoRegisterDTO)).Returns(video);
            _videoRepositoryMock.Setup(repo => repo.Update(video)).ReturnsAsync(video);
            _inventoryRepositoryMock.Setup(repo => repo.GetQty(1)).ReturnsAsync(10);
            _dtoServiceMock.Setup(service => service.MapVideoToVideoDTO(video, 10)).Returns(videoDTO);

            // Act
            var result = await _videoService.UpdateVideoDetails(videoRegisterDTO, 1);

            // Assert
            Assert.That(result.Id, Is.EqualTo(videoDTO.Id));
            Assert.That(result.Tittle, Is.EqualTo(videoDTO.Tittle));
            Assert.That(result.Stock, Is.EqualTo(videoDTO.Stock));
        }
        [Test]
        public void UpdateVideoDetailsPaasTest_ThrowsNoSuchItemInDbException()
        {
            // Arrange
            var videoRegisterDTO = new VideoRegisterDTO();

            _videoRepositoryMock.Setup(repo => repo.CheckVideoExistById(1)).ReturnsAsync(false);

            // Act & Assert
            Assert.ThrowsAsync<NoSuchItemInDbException>(() => _videoService.UpdateVideoDetails(videoRegisterDTO, 1));
        }

        [Test]
        public async Task GetVideosByGenreFailTest_ReturnsVideoDTOs()
        {
            // Arrange
            var videos = new List<Video>
            {
                new Video { Id = 1, Tittle = "Action Video", Genre = Genre.Action },
                new Video { Id = 2, Tittle = "Comedy Video", Genre = Genre.Comedy }
            };
            var videoDTOs = new List<VideoDTO>
            {
                new VideoDTO { Id = 1, Tittle = "Action Video", Genre = Genre.Action, Stock = 10 },
            };

            _videoRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(videos);
            _inventoryRepositoryMock.Setup(repo => repo.GetQty(It.IsAny<int>())).ReturnsAsync(10);
            _dtoServiceMock.Setup(service => service.MapVideoToVideoDTO(It.IsAny<Video>(), It.IsAny<int>())).Returns((Video v, int qty) => new VideoDTO { Id = v.Id, Tittle = v.Tittle, Genre = v.Genre, Stock = qty });

            // Act
            var result = await _videoService.GetVideosByGenre(Genre.Action);

            // Assert
            Assert.That(result.Count, Is.EqualTo(videoDTOs.Count));
            for (int i = 0; i < videoDTOs.Count; i++)
            {
                Assert.That(result[i].Id, Is.EqualTo(videoDTOs[i].Id));
                Assert.That(result[i].Tittle, Is.EqualTo(videoDTOs[i].Tittle));
                Assert.That(result[i].Genre, Is.EqualTo(videoDTOs[i].Genre));
                Assert.That(result[i].Stock, Is.EqualTo(videoDTOs[i].Stock));
            }
        }

        [Test]
        public void GetVideosByGenre_NoVideos_ThrowsNoItemsInDbException()
        {
            // Arrange
            _videoRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync((IEnumerable<Video>)null);

            // Act & Assert
            Assert.ThrowsAsync<NoItemsInDbException>(() => _videoService.GetVideosByGenre(Genre.Action));
        }

    }
}
