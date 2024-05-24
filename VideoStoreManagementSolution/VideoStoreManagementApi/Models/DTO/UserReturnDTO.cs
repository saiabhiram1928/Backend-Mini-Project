using VideoStoreManagementApi.Models.Enums;

namespace VideoStoreManagementApi.Models.DTO
{
    public class UserReturnDTO
    {
        public string Email { get; set; }
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string Verified { get; set; }    
        public MembershipType MembershipType { get; set; }
        public Address Address { get; set; }
       
    }
}
