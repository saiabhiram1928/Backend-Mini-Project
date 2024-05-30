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
        public Task<DeliveryStatus> ChangeDeliveryStatus(int OrderId, DeliveryStatus deliveryStatus);
        public Task<string> MarkPaymentDoneCod(int OrderId);    
    }
}
