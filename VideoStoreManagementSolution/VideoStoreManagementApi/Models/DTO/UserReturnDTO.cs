using System.Text.Json.Serialization;
using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models.DTO
{
    public class UserReturnDTO
    {
        public string Email { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public bool Verified { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public MembershipType MembershipType { get; set; }
        public string Token { get; set; } =string.Empty;
        public Role Role { get; set; }
    }
}
