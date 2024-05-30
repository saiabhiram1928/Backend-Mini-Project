using System.Text.Json.Serialization;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models.DTO
{
    public class UserProfileEditDTO
    {
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
