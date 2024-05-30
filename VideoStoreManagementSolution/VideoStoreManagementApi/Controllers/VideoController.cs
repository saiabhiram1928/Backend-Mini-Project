using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoStoreManagementApi.CustomAction;
using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly IVideoService _videoService;

        public VideoController(IVideoService videoService)
        {
            _videoService = videoService;
        }
        [Authorize]
        [HttpGet("VideoPagination")]
        [ProducesResponseType(typeof(IList<VideoDTO>) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<VideoDTO>>> GetAllVideoApi(int pageNumber , int pageSize)
        {
            try
            {
                var list = await _videoService.GetAllVideos(pageNumber,pageSize);
                return Ok(list);
            }catch(NoItemsInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
        }
        [Authorize]
        [HttpGet("GetVideoById")]
        [ProducesResponseType(typeof(VideoDTO) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VideoDTO >> GetVideoByIdApi([FromQuery] int id)
        {
            try
            {
                var video = await _videoService.GetVideoById(id);
                return Ok(video);
            }catch(NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
        }
        [Authorize]
        [HttpGet("Search")]
        [ProducesResponseType(typeof(IEnumerable<VideoDTO>) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest)]
        
        public async Task<ActionResult<IList<VideoDTO>>> SearchApi(string name, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest(new ErrorDTO(400, "Search query cannot be empty."));
            }
            var videos = await _videoService.Search(name, pageNumber, pageSize);
            if(videos == null || videos.Count == 0)
            {
                return Ok(new { Message = "No videos found matching your search criteria" });
            }
            return Ok(videos);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("AddVideo")]
        [ProducesResponseType(typeof(VideoDTO), StatusCodes.Status200OK )]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status400BadRequest )]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError )]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status409Conflict )]

        public async Task<ActionResult<VideoDTO>> AddVideoApi([FromBody] VideoRegisterDTO videoRegisterDTO)
        {
            
            try
            {
                var video = await _videoService.AddVideo(videoRegisterDTO);
                return Ok(video);
            }catch(DbException ex)
            {
                return  InternalServerError<VideoDTO>.Action(new ErrorDTO(500, ex.Message));
            }catch(DuplicateItemException ex)
            {
                return Conflict(new ErrorDTO(409,ex.Message));
            }
            
        }

        [Authorize(Roles = "Admin,Employee")]
        [HttpPost("UpdateVideoDetails")]
        [ProducesResponseType(typeof(VideoDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VideoDTO>> UpdateVideoDetailsApi([FromBody] VideoRegisterDTO videoRegisterDTO , [FromQuery] int VideoId)
        {

            try
            {
                var video = await _videoService.UpdateVideoDetails(videoRegisterDTO , VideoId);
                return Ok(video);
            }
            catch (DbException ex)
            {
                return InternalServerError<VideoDTO>.Action(new ErrorDTO(500, ex.Message));
            }
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }

        }
        [Authorize]
        [HttpGet("GetVideoByGenre")]
        [ProducesResponseType(typeof(IList<VideoDTO>) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<VideoDTO>>> GetVideoByGenreApi(Genre genre)
        {
            try
            {
                return Ok(await _videoService.GetVideosByGenre(genre)); 
            }catch(NoItemsInDbException ex)
            {
                return NotFound(new ErrorDTO(404 , ex.Message));
            }
        }
    }
}
