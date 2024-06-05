using System.ComponentModel.DataAnnotations.Schema;

namespace VideoStoreManagementApi.Models.DTO
{
    public class AddressDTO
    {
        public int Id { get; set; }
        
        public string Area { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        public int Zipcode { get; set; }
        public bool PrimaryAdress { get; set; }
       
    }
}
