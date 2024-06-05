using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models.DTO
{
    public class UserProfileEditDTO
    {

        [Required]
        [RegularExpression(@".*\S+.*", ErrorMessage = "First Name cannot be empty or whitespace.")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(@".*\S+.*", ErrorMessage = "Last Name cannot be empty or whitespace.")]
        public string LastName { get; set; }
    }
}
