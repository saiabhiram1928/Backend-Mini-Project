using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _userService;
        public UserAuthController(IUserAuthService userService) 
        {
            _userService = userService;
        }
        [HttpPost("login")]
        [ProducesResponseType(typeof(UserReturnDTO) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserReturnDTO>> LoginApi([FromBody] UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _userService.Login(userLoginDTO);
                return Ok(user);
            }catch(NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404,ex.Message));
            }catch(UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }
        }
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserReturnDTO) ,StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ErrorDTO) ,StatusCodes.Status500InternalServerError)]
        
        public async Task<ActionResult<UserReturnDTO>> RegisterApi( UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var user = await _userService.Register(userRegisterDTO);
                return Ok(user);
            }catch(DuplicateItemException ex)
            {
                return Conflict(new ErrorDTO(409, ex.Message));
            }
            catch(DbException ex)
            {
                return BadRequest(new ErrorDTO(500, ex.Message));
            }
        }
    }
}
