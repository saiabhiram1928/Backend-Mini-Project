using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoStoreManagementApi.CustomAction;
using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [Authorize]
        [HttpPost("AddToCart")]
        [ProducesResponseType(typeof(CartDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status409Conflict)]

        public async Task<ActionResult<CartDTO>> AddToCartApi(int videoId, int qty)
        {
            try
            {
                var res = await _cartService.AddToCart(videoId,qty);
                return Ok(res);
            }catch(UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message)); 
            }catch(NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }catch(DbException ex)
            {
                return InternalServerError<CartDTO>.Action(new ErrorDTO(500,ex.Message));
            }catch(QunatityOutOfStockException ex)
            {
                return Conflict(new ErrorDTO(409, ex.Message));
            }
        }
        [Authorize]
        [HttpGet("ViewCart")]
        [ProducesResponseType(typeof(CartDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CartDTO>> ViewCartApi()
        {
            try
            {
                var res = await _cartService.ViewCart();
                return Ok(res);
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }catch(NoItemsInCartException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
            
        }
        [Authorize]
        [HttpPatch("UpdateCart")]
         public async Task<ActionResult<CartDTO>> EditCartApi(int cartItemId, int newQty)
        {
            try
            {
                var res = await _cartService.EditCart(cartItemId,newQty);
                return Ok(res);
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }
            catch (NoItemsInCartException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(new ErrorDTO(403,ex.Message));
            }catch(DbException ex)
            {
                return InternalServerError<CartDTO>.Action(new ErrorDTO(500, ex.Message));
            }
            
        }
        [Authorize]
        [HttpDelete("DeleteCartItem")]
        public async Task<ActionResult<CartDTO>> DeleteCartApi(int cartItemId)
        {
            try
            {
                var res = await _cartService.DeleteCartItem(cartItemId);
                return Ok(res);
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }
            catch (NoItemsInCartException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ErrorDTO(403, ex.Message));
            }
            catch (DbException ex)
            {
                return InternalServerError<CartDTO>.Action(new ErrorDTO(500, ex.Message));
            }

        }

    }
}
