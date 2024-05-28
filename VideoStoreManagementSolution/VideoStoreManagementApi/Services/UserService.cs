using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;
using VideoStoreManagementApi.Repositories;

namespace VideoStoreManagementApi.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDTOService _dtoService;
        private readonly IAddressRepository _addressRepository;

        public UserService(ITokenService tokenService , ICustomerRepository customerRepository , IUserRepository userRepository, IDTOService dTOService  , IAddressRepository addressRepository) 
        {
            _tokenService = tokenService;
            _customerRepository = customerRepository;
            _userRepository = userRepository; 
            _dtoService = dTOService;
            _addressRepository = addressRepository; 
        }
        public Task<Address> AddAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePassword(string newPasswd)
        {
            ;throw new NotImplementedException();
        }

        public Task<Address> DeleteAdress(Address address)
        {
            throw new NotImplementedException();
        }

        public Task<Address> EditAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public async Task<UserReturnDTO> EditProfile(UserProfileEditDTO userProfileEditDTO)
        {
            var id = _tokenService.GetUidFromToken();
            if (id == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var user = await _userRepository.GetById((int)id);
            if(user == null)
            {
                throw new NoSuchItemInDbException("User with Given Id Doesnt Exist");
            }
            user.FirstName = userProfileEditDTO.FirstName;
            user.LastName = userProfileEditDTO.LastName;
            user = await _userRepository.Update(user);
            var customer = await _customerRepository.GetById(user.Uid);
            var userReturnDTo = _dtoService.MapUserToUserReturnDTO(customer, "");
            return userReturnDTo;

        }

        public async Task<UserReturnDTO> ViewProfile()
        {
            var id = _tokenService.GetUidFromToken();
            if(id == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            
            var customer = await _customerRepository.GetById((int)id);
            if(customer == null)
            {
                throw new NoSuchItemInDbException("Customer With Given Id Doesnt Eixst");
            }
           
            var userReturnDTO = _dtoService.MapUserToUserReturnDTO(customer,"");
            
            return userReturnDTO;

        }

        public async Task<IList<AddressDTO>> ViewAddress()
        {
            var id = _tokenService.GetUidFromToken();
            if (id == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var checkUserExist = await _userRepository.CheckUserExist((int)id);
               if(checkUserExist == false)
            {
                throw new NoItemsInDbException("User with givenId Doesnt Exist");
            }
            var address = await _addressRepository.GetAllAdressOfUser((int)id);
            List<AddressDTO> addressDTOList = new List<AddressDTO>();
            foreach(var a  in address)
            {
               addressDTOList.Add (_dtoService.MapAddressToAddressDTO(a));
            }
            return addressDTOList;
        }
        
    }
}
