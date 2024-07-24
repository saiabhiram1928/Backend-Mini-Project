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
        [ProducesResponseType(typeof(OrderStatus) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderStatus>> ChangeOrderStatusApi(int orderId , OrderStatus deliveryStatus)
        {
            try
            {
                var res = await _orderService.ChangeOrderStatusForAdmin(orderId, deliveryStatus);
                return Ok(res);
            }
           
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
           

        }       
        [Authorize(Roles = "Admin,Employee")]
        [HttpPatch("MarkPaymentDone")]
        [ProducesResponseType(typeof(MessageDTO) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageDTO>> MarkPaymentDoneApi(int orderId)
        {
            try
            {
                var res = await _orderService.MarkPaymentDoneCodForAdmin(orderId);
                return Ok(res);
            }
           
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
           

        }
        [Authorize]
        [HttpPost("CancelOrder")]
        [ProducesResponseType(typeof(MessageDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<MessageDTO>> CancelOrderApi(int orderId)
        {
            try
            {
                var res = await _orderService.CancelOrder(orderId);
                return Ok(res);
            }

            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ErrorDTO(403, ex.Message));
            }
            catch (DbException ex)
            {
                return InternalServerError<MessageDTO>.Action(new ErrorDTO(500, ex.Message));
            }catch(OrderCantBeCancelledException ex)
            {
                return Conflict(new ErrorDTO(409, ex.Message));
            }
        }

        [Authorize]
        [HttpGet("CheckRefundStatus")]
        [ProducesResponseType(typeof(RefundDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<RefundDTO>> CheckRefundStatusApi(int orderId)
        {
            try
            {
                var res = await _orderService.CheckRefundStatus(orderId);
                return Ok(res);
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new ErrorDTO(401, ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ErrorDTO(403, ex.Message));
            }
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
        }
        [Authorize(Roles = "Admin , Employee")]
        [HttpPost("IssueRefund")]
        [ProducesResponseType(typeof(MessageDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MessageDTO>> IssueRefundApi(int orderId)
        {
            try
            {
                var res = await _orderService.IssuseRefundForAdmin(orderId);
                return Ok(res);
            }
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
        }
        [Authorize(Roles = "Admin , Employee")]
        [HttpGet("ViewAllRefunds")]
        [ProducesResponseType(typeof(IList<Refund>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<Refund>>> ViewAllfundsApi()
        {
            try
            {
                var res = await _orderService.ViewAllRefundsForAdmin();
                return Ok(res);
            }
            catch (Exception ex)
            {
                return InternalServerError<IList<Refund>>.Action(new ErrorDTO(500, ex.Message));
            }
        }
         [Authorize(Roles = "Admin , Employee")]
        [HttpGet("ViewAllOrderBasedOnStatus")]
        [ProducesResponseType(typeof(IList<Refund>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IList<Refund>>> ViewAllOrderBasedOnStatusApi(OrderStatus orderStatus)
        {
            try
            {
                var res = await _orderService.ViewAllOrdersAreBasedOnStatusdForAdmin(orderStatus);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return InternalServerError<IList<Refund>>.Action(new ErrorDTO(500, ex.Message));
            }
        }
        
        [Authorize(Roles = "Admin , Employee")]
        [HttpGet("GetOrderByIdAdmin")]
        [ProducesResponseType(typeof(IList<Refund>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<Refund>>> GetOrderByIdAdminApi(int orderId)
        {
            try
            {
                var res = await _orderService.GetOrderbyIdForAdmin(orderId);
                return Ok(res);
            }
           
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }
        }
         
        [Authorize(Roles = "Admin , Employee")]
        [HttpGet("GetAllOrders")]
        [ProducesResponseType(typeof(IList<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<Order>>> GetAllOrdersApi(int pageNumber, int pageSize)
        {
            try
            {
                var res = await _orderService.GetAllOrdersForAdmin(pageNumber, pageSize);
                return Ok(res);
            }
           
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }catch(ArgumentException ex)
            {
                return BadRequest(new ErrorDTO(403, ex.Message));
            }
        }
        [Authorize(Roles = "Admin , Employee")]
        [HttpPost("ReturnVideos")]
        [ProducesResponseType(typeof(Rental), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDTO), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Rental>> ReturnVideos (int orderId)
        {
            try
            {
                var res = await _orderService.ReturnVideos(orderId);
                return Ok(res);
            }
           
            catch (NoSuchItemInDbException ex)
            {
                return NotFound(new ErrorDTO(404, ex.Message));
            }catch(Exception ex)
            {
                return BadRequest(new ErrorDTO(403, ex.Message));
            }
        }
        
       

    }
}
