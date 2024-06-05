using VideoStoreManagementApi.Models.Enums;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.Interfaces.Services
{
    public interface IOrderService
    {
        public Task<OrderDTO> MakePayment(PaymentType paymentType, int addressId);
        public Task<IList<OrderDTO>> GetOrdersOfUser();
        public Task<OrderDTO> GetOrderById(int orderId);
        public Task<OrderStatus> ChangeOrderStatusForAdmin(int OrderId, OrderStatus deliveryStatus);
        public Task<MessageDTO> MarkPaymentDoneCodForAdmin(int OrderId);
        public  Task<MessageDTO> CancelOrder(int orderId);
        public  Task<RefundDTO> CheckRefundStatus(int orderId);
        public Task<MessageDTO> IssuseRefundForAdmin(int orderId);
        public  Task<IList<Refund>> ViewAllRefundsForAdmin();
        public  Task<IList<Order>> ViewAllOrdersAreBasedOnStatusdForAdmin(OrderStatus orderStatus);
        public  Task<Order> GetOrderbyIdForAdmin(int orderId);
        public Task<IList<Order>> GetAllOrdersForAdmin(int pageNumber, int pageSize);
    }
}
