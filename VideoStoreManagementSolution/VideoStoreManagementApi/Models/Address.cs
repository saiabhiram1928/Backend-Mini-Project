using System.ComponentModel.DataAnnotations.Schema;

namespace VideoStoreManagementApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Area { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        public int Zipcode { get; set; }    
        public bool PrimaryAdress { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}
