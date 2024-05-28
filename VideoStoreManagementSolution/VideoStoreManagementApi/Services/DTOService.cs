using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.Services
{
    public class DTOService : IDTOService
    {
        public AddressDTO MapAddressToAddressDTO(Address a)
        {
          AddressDTO addressDTO = new AddressDTO();
            addressDTO.Area = a.Area;
            addressDTO.City = a.City;
            addressDTO.PrimaryAdress = a.PrimaryAdress;
            addressDTO.Zipcode = a.Zipcode;
            addressDTO.Id = a.Id;
            addressDTO.State = a.State;
            return addressDTO;
        }

        public Customer MapUserRegisterDTOToCustomer(UserRegisterDTO userRegisterDTO, User user)
        {
            Customer customer = new Customer();
            customer.MembershipType = Models.Enums.MembershipType.Basic;
            customer.Uid = user.Uid;
            
            return customer;
        }

        public User MapUserRegisterDTOToUser(UserRegisterDTO userRegisterDTO, byte[] salt, byte[] passwd)
        {
            User user = new User();
         
            user.FirstName = userRegisterDTO.FirstName;
            user.LastName = userRegisterDTO.LastName;
            user.Password = passwd;
            user.Salt = salt;
            user.Verified = true;
            user.Email = userRegisterDTO.Email;
           user.Role = Models.Enums.Role.Customer;
            return user;
           
        }

        public UserReturnDTO MapUserToUserReturnDTO(Customer user, string Token)
        {
            UserReturnDTO userReturnDTO = new UserReturnDTO();
            userReturnDTO.FirstName = user.User.FirstName;
            userReturnDTO.LastName = user.User.LastName;
            userReturnDTO.Email = user.User.Email;
            userReturnDTO.Verified = user.User.Verified;
            userReturnDTO.MembershipType = user.MembershipType;
            userReturnDTO.Role = user.User.Role;
            userReturnDTO.Token = Token;
            return userReturnDTO;
        }

        public Video MapVideoRegisterDTOToVideo(VideoRegisterDTO videoRegisterDTO)
        {
           Video video = new Video();   
           video.Tittle = videoRegisterDTO.Tittle;
            video.Price = videoRegisterDTO.Price;
            video.Description = videoRegisterDTO.Description;
            video.Director = videoRegisterDTO.Director;
            video.Genre = videoRegisterDTO.Genre;
            video.ReleaseDate = videoRegisterDTO.ReleaseDate;
            return video;
        }
    }
}
