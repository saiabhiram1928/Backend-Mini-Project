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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }
        [Authorize]
        [HttpPost("MakePayment")]
        [ProducesResponseType(typeof(OrderDTO) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status500InternalServerError)]
     
        public async Task<ActionResult<OrderDTO>> MakePaymentApi(PaymentType paymentType , int addressId)
        {
            try
            {
                var res = await _orderService.MakePayment(paymentType , addressId);
                return Ok(res);
            }catch(UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401,ex.Message));
            }catch(NoItemsInCartException ex)
            {
               return NotFound(new ErrorDTO(404, ex.Message));
            }catch(NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }catch(QunatityOutOfStockException ex)
            {
                return BadRequest(new ErrorDTO(404,ex.Message));
            }catch(DbException ex)
            {
                return InternalServerError<OrderDTO>.Action(new ErrorDTO(500, ex.Message));
            }
        }
        [Authorize]
        [HttpGet("ViewOrders")]
        [ProducesResponseType(typeof(IList<OrderDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(IList<OrderDTO>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<OrderDTO>>> ViewOrdersApi()
        {
            try
            {
                var res = await _orderService.GetOrdersOfUser();
                return Ok(res);
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }
            
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }

        }

        [Authorize]
        [HttpGet("ViewOrderById")]
        [ProducesResponseType(typeof(IList<OrderDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(IList<OrderDTO>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(IList<OrderDTO>), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IList<OrderDTO>>> ViewOrderByIdApi(int orderId)
        {
            try
            {
                var res = await _orderService.GetOrderById(orderId);
                return Ok(res);
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }

            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }  catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ErrorDTO(403, ex.Message));
            }

        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPatch("ChangeDeliveryStatus")]
        [ProducesResponseType(typeof(DeliveryStatus) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeliveryStatus>> ChangeDeliveryStatusApi(int orderId , DeliveryStatus deliveryStatus)
        {
            try
            {
                var res = await _orderService.ChangeDeliveryStatus(orderId, deliveryStatus);
                return Ok(res);
            }
           
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
           

        }       
        [Authorize(Roles = "Admin,Employee")]
        [HttpPatch("MarkPaymentDone")]
        [ProducesResponseType(typeof(DeliveryStatus) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> MarkPaymentDoneApi(int orderId)
        {
            try
            {
                var res = await _orderService.MarkPaymentDoneCod(orderId);
                return Ok(res);
            }
           
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
           

        }
    }
}
