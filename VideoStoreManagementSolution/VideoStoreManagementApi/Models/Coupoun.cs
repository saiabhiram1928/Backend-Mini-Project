using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models
{
    public class Coupoun
    {
      
            public int Id { get; set; }
            public string CouponCode { get; set; }
            public DiscountType DiscountType { get; set; }
            public float DiscountValue { get; set; }
            public DateTime ExpirationDate { get; set; }
            public int UsageLimit { get; set; }
            public int TimesUsed { get; set; }
            public decimal MinimumPurchaseAmount { get; set; }
            public CoupounStatus Status { get; set; }
        
    }
}
