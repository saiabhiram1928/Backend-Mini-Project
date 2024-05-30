using System.ComponentModel.DataAnnotations;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models
{
    public class User
    {
        [Key] 
        public int Uid { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public Role Role { get; set; }
        public bool Verified {  get; set; } 
    }
   
}
