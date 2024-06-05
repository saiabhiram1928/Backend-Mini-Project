using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Models.Enums;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VideoStoreManagementApi.Interfaces.Repositories;

namespace VideoStoreManagementTest.RepositoriesTest
{
    internal class VideoRepositoryTest
    {
        private VideoStoreContext _context;
        private IVideoRepository _videoRepository;

        [SetUp]
        public void SetUp()
        {
            DbContextOptionsBuilder<VideoStoreContext> _optionsBuilder = new DbContextOptionsBuilder<VideoStoreContext>().UseInMemoryDatabase(databaseName: "db_test");
            _context = new VideoStoreContext(_optionsBuilder.Options);
            _videoRepository = new VideoRepository(_context);

        }
        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        [Test]  
        public async Task AddVideoPassTest()
        {
            // Arrange
            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };

            // Act
            var addedVideo = await _videoRepository.Add(video);

            // Assert
            Assert.IsNotNull(addedVideo);
            Assert.That(addedVideo.Tittle, Is.EqualTo(video.Tittle));
        }
        [Test]
        public async Task AddVideoFailTest()
        {
            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };
            await _videoRepository.Add(video);

            var ex = Assert.ThrowsAsync<System.ArgumentException>(async () =>
            {
                await _videoRepository.Add(video);
            });
            Assert.IsNotNull(ex);
        }
        [Test]
        public async Task UpdateVideoPassTest()
        {
            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };
            video = await _videoRepository.Add(video);

            video.Price = 15.99f;
            var updatedVideo = await _videoRepository.Update(video);

            Assert.IsNotNull(updatedVideo);
            Assert.That(updatedVideo.Price, Is.EqualTo(15.99f));
        }
        [Test]
        public async Task UpdateVideoFailTest()
        {
            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };
            video = await _videoRepository.Add(video);

            var ex = Assert.ThrowsAsync<InvalidOperationException > (async () =>
            {
                video.Id = 100;
                await _videoRepository.Update(video);
            });
            Assert.IsNotNull(ex);
        }
        [Test]
        public async Task DeleteVideoPassTest()
        {
            //Arrange
            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };
            video = await _videoRepository.Add(video);

            //Act
            var result = await _videoRepository.Delete(video.Id);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task DeleteVideoFailTest()
        {
            //Arrange && Act
            var result = await _videoRepository.Delete(100);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetVideoByIdPassTest()
        {
            //Arrange
            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };
            video = await _videoRepository.Add(video);
            
            //Act
            var result = await _videoRepository.GetById(video.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.That(result.Tittle, Is.EqualTo(video.Tittle));
        }
        [Test]
        public async Task GetVideoByIdFailTest()
        {
            //Arrange && Act
            var result = await _videoRepository.GetById(100);
            //Assert
            Assert.IsNull(result);
        }
        [Test]
        public async Task GetAllVideosPassTest()
        {
            var videos = new List<Video>
            {
                new Video
                {
                    Tittle = "Test Video 1",
                    Description = "Test Description 1",
                    Genre = Genre.Action,
                    Director = "Test Director 1",
                    ReleaseDate = DateTime.Now,
                    Price = 10.99f
                },
                new Video
                {
                    Tittle = "Test Video 2",
                    Description = "Test Description 2",
                    Genre = Genre.Comedy,
                    Director = "Test Director 2",
                    ReleaseDate = DateTime.Now,
                    Price = 12.99f
                }
            };
            videos[1] = await _videoRepository.Add(videos[1]);
            videos[0] = await _videoRepository.Add(videos[0]);

            var result = await _videoRepository.GetAll();

            Assert.IsNotNull(result);
            CollectionAssert.AreEquivalent(videos, result);
        }
        [Test]
        public async Task GetAllVideosFailTest()
        {
            var result = await _videoRepository.GetAll();

            Assert.IsEmpty(result);
        }
        [Test]
        public async Task QueryContainsPassTest()
        {
            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };
            await _videoRepository.Add(video);

            var result = await _videoRepository.QueryContains("Test", 1, 10);

            Assert.IsNotNull(result);
            Assert.That(result.Count, Is.GreaterThan(0));
            Assert.That(result.First().Tittle, Is.EqualTo(video.Tittle));
        }
        [Test]
        public async Task QueryContainsFailTest()
        {

            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };
            await _videoRepository.Add(video);

            var result = await _videoRepository.QueryContains("Non-existing", 1, 10);

            Assert.IsEmpty(result);
        }
        [Test]
        public async Task GetPriceOfVideoPassTest()
        {
            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };
            video = await _videoRepository.Add(video);

            var price = await _videoRepository.GetPriceOfVideo(video.Id);

            Assert.That(price, Is.EqualTo(10.99f));
        }
        [Test]
        public void GetPriceOfVideoFailTest()
        {
          

           var ex = Assert.ThrowsAsync<NullReferenceException>(async () =>
           {
               var price = await _videoRepository.GetPriceOfVideo(100);
           });

            Assert.IsNotNull(ex);
            Assert.That(ex.Message, Is.EqualTo("No Such Video Exist"));
        }
        [Test]
        public async Task CheckVideoExistByTittlePassTest()
        {
            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };
            await _videoRepository.Add(video);

            var exists = await _videoRepository.CheckVideoExistByTittle("Test Video");

            Assert.IsTrue(exists);
        }


        [Test]
        public async Task CheckVideoExistByTittleFailTest()
        {
            var exists = await _videoRepository.CheckVideoExistByTittle("Non-existing");

            Assert.IsFalse(exists);
        }
        [Test]
        public async Task CheckVideoExistByIdPassTest()
        {
            var video = new Video
            {
                Tittle = "Test Video",
                Description = "Test Description",
                Genre = Genre.Action,
                Director = "Test Director",
                ReleaseDate = DateTime.Now,
                Price = 10.99f
            };
            video = await _videoRepository.Add(video);

            var exists = await _videoRepository.CheckVideoExistById(video.Id);

            Assert.IsTrue(exists);
        }
        [Test]
        public async Task CheckVideoExistByIdFailTest()
        {
            var exists = await _videoRepository.CheckVideoExistById(100);

            Assert.IsFalse(exists);
        }
        [Test]
        public async Task GetAllByPaginationPassTest()
        {
            var videos = new List<Video>
            {
                new Video
                {
                    Tittle = "Test Video 1",
                    Description = "Test Description 1",
                    Genre = Genre.Action,
                    Director = "Test Director 1",
                    ReleaseDate = DateTime.Now,
                    Price = 10.99f
                },
                new Video
                {
                    Tittle = "Test Video 2",
                    Description = "Test Description 2",
                    Genre = Genre.Comedy,
                    Director = "Test Director 2",
                    ReleaseDate = DateTime.Now,
                    Price = 12.99f
                }
            };
            videos[1] = await _videoRepository.Add(videos[1]);
            videos[0] = await _videoRepository.Add(videos[0]);

            var result = await _videoRepository.GetAllByPagination(1, 1);

            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(1));
        }
        [Test]
        public async Task GetAllByPaginationFailTest()
        {
            var result = await _videoRepository.GetAllByPagination(1, 1);

            Assert.IsEmpty(result);
        }
    }
}
