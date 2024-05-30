using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoStoreManagementApi.CustomAction;
using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService; 
        }
        [Authorize]
        [HttpGet("ViewProfile")]
        [ProducesResponseType(typeof(UserReturnDTO)  , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO)  , StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDTO)  , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReturnDTO>> ProfileApi()
        {
            try
            {
                var userReturnDTO = await _userService.ViewProfile();
                return Ok(userReturnDTO);
            }
            catch(UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401,ex.Message));
            }catch(NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404,ex.Message));
            }
        }
        [Authorize]
        [HttpGet("ViewAdressesOfUser")]
        [ProducesResponseType(typeof(IList<AddressDTO>) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult<IList<AddressDTO>>> ViewAddressesApi()
        {
            try
            {
                var address = await _userService.ViewAddress();
                return Ok(address);
            }catch(NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }catch(UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }
        }

        [Authorize]
        [HttpPut("EditProfile")]
        [ProducesResponseType(typeof(UserRegisterDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserReturnDTO>> EditProfileApi([FromBody] UserProfileEditDTO userProfileEditDTO)
        {
            try
            {
                var userReturnDTO = await _userService.EditProfile(userProfileEditDTO);
                return Ok(userReturnDTO);
            }
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }catch(DbException ex)
            {
                return InternalServerError<UserReturnDTO>.Action(new ErrorDTO(500, ex.Message));
            }
        }
        [Authorize] 
        [HttpPut("UpdateAddress")]
        [ProducesResponseType(typeof(AddressDTO),StatusCodes.Status200OK )]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status404NotFound )]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status401Unauthorized )]
        [ProducesResponseType(typeof(ErrorDTO),StatusCodes.Status500InternalServerError )]
        public async Task<ActionResult<AddressDTO>> EditAddressApi([FromBody] AddressDTO addressDTO)
        {
            try
            {
                var res = await _userService.EditAddress(addressDTO);
                return Ok(res);
            }
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }catch(DbException ex)
            {
                return InternalServerError<AddressDTO>.Action(new ErrorDTO(500, ex.Message));
            }
        }
        [Authorize]
        [HttpDelete("DeleteAddress")]
        [ProducesResponseType(typeof(string) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> DeleteAddressApi(int id)
        {
            try
            {
                var res = await _userService.DeleteAddress(id);
                return Ok(res);
            }
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }
            catch (DbException ex)
            {
                return InternalServerError<string>.Action(new ErrorDTO(500, ex.Message));
            }
        }
        [Authorize]
        [HttpPost("AddAddress")]
        [ProducesResponseType(typeof(AddressRegisterDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AddressDTO>> AddAddressApi([FromBody] AddressRegisterDTO addressRegisterDTO)
        {
            try
            {
                
                var res = await _userService.AddAddress(addressRegisterDTO);    
                return Ok(res); 
            }
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }
            catch (DbException ex)
            {
                return InternalServerError<AddressDTO>.Action(new ErrorDTO(500, ex.Message));
            }
        }
        [Authorize]
        [HttpPatch("ChangePassword")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<string>> ChangePasswdApi(string oldPasswd, string newPasswd)
        {
            try
            {
                var res = await _userService.ChangePassword(newPasswd, oldPasswd);
                return Ok(res);
            }
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }
            catch (DbException ex)
            {
                return InternalServerError<string>.Action(new ErrorDTO(500, ex.Message));
            }catch(UnauthorizedUserException ex)
            {
                return BadRequest(new ErrorDTO(403, ex.Message));
            }
        }

        
    }
}
