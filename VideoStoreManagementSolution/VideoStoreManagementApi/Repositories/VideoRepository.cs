using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using VideoStoreManagementApi.Contexts;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Models;
using static System.Net.Mime.MediaTypeNames;

namespace VideoStoreManagementApi.Repositories
{
    public class VideoRepository : CRUDRepository<int, Video>, IVideoRepository
    {
        public VideoRepository(VideoStoreContext context) : base(context) { }
        public override async Task<IEnumerable<Video>> GetAll()
        {
            return await _context.Videos.ToListAsync();
        }

        public override async Task<Video> GetById(int key)
        {
            var item =await _context.Videos.SingleOrDefaultAsync(x => x.Id == key);
            return item;
        }

        public async Task<bool> CheckVideoExistByTittle(string text)
        {
           return await _context.Videos.AnyAsync(v => v.Tittle.ToLower() == text.ToLower());
        }

        public async Task<IList<Video>> QueryContains(string text, int pageNumber, int pageSize)
        {
            var textLower = text.ToLower();
            
            var videos = await _context.Videos
                .Where(v =>
                    v.Tittle.ToLower().Contains(textLower) ||
                    v.Description.ToLower().Contains(textLower) ||
                    v.Director.ToLower().Contains(textLower) ||
                    v.Genre.Equals(textLower)
                    ).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();
            return videos;
        }

        public async Task<bool> CheckVideoExistById(int id)
        {
            return await _context.Videos.AnyAsync(v => v.Id == id);
        }
        public async Task<IEnumerable<Video>> GetAllByPagination(int pageNumber,int pageSize)
        {
            return await _context.Videos.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<float> GetPriceOfVideo(int id)
        {
            var video = await _context.Videos.
                SingleOrDefaultAsync(v => v.Id == id);
            return video.Price;
        }
    }
}
