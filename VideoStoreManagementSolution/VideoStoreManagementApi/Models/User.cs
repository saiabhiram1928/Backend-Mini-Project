using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models
{
    public class User
    {
        [Key] 
        public int Uid { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        [Required]
        public byte[] Salt { get; set; }
        [Required]
        public Role Role { get; set; }
        public bool Verified {  get; set; } 
    }
   
}
