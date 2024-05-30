namespace VideoStoreManagementApi.Models.DTO
{
    public class AddressRegisterDTO
    {
        public string Area { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        public int Zipcode { get; set; }
        public bool PrimaryAdress { get; set; }
    }
}
