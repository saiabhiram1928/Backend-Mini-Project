namespace VideoStoreManagementApi.Models.DTO
{
    public class CartDTO
    {
        public int cartId {  get; set; }    
        public float TotalPrice { get; set; }
        public int customerId { get; set; } 
        public IList<CartItemDTO> CartItems { get; set; } = new List<CartItemDTO>();
    }
}
