using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models
{
    public class Customer
    {
        [Key]
        
        public int Uid {  get; set; }
        [ForeignKey(nameof(Uid))]
        public User User { get; set; }
        public MembershipType MembershipType { get; set; }
       public ICollection<Address> Address { get; set; } = new List<Address>();
        public ICollection<Order> Orders { get; set; }  = new List<Order>();    
    }

}
