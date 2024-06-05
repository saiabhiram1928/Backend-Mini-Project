using System.ComponentModel.DataAnnotations;

namespace VideoStoreManagementApi.Models.DTO
{
    public class AddressRegisterDTO
    {
        [Required]
        public string Area { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int Zipcode { get; set; }
        [Required]
        public bool PrimaryAdress { get; set; }
    }
}
