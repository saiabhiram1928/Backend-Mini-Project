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
        private readonly IHashService _hashService;

        #region Constructor
        public UserService(ITokenService tokenService , ICustomerRepository customerRepository , IUserRepository userRepository, IDTOService dTOService  , IAddressRepository addressRepository,IHashService hashService) 
        {
            _tokenService = tokenService;
            _customerRepository = customerRepository;
            _userRepository = userRepository; 
            _dtoService = dTOService;
            _addressRepository = addressRepository; 
            _hashService = hashService;
        }
        #endregion


        #region AddAddress
        /// <summary>
        /// Add Address to Database
        /// </summary>
        /// <param name="addressRegisterDTO"></param>
        /// <returns>AddressDTO</returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="DbException"></exception>
        public async Task<AddressDTO> AddAddress(AddressRegisterDTO addressRegisterDTO)
        {
            var addressDTO = _dtoService.MapAddressRegisterDTOTOAddressDTO(addressRegisterDTO);
            var uid = _tokenService.GetUidFromToken();
            if (uid == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var checkUserExist = await _userRepository.CheckUserExist((int)uid);
            if(!checkUserExist)
            {
                throw new NoSuchItemInDbException("The User With Given Doenst exist");
            }

            Address address = new Address();
            address.CustomerId = (int)uid;
             if(addressDTO.PrimaryAdress == true)
             {
                await _addressRepository.MakePrimaryAddressFalse((int)uid);
              }
            address = _dtoService.MapAddressDTOToAddress(addressDTO, address);
            address = await _addressRepository.Add(address);
            if (address == null) throw new DbException();
            var addressDTOReturn = _dtoService.MapAddressToAddressDTO(address);
            return addressDTOReturn;
            
        }
        #endregion

        #region ChangePassword
        /// <summary>
        /// Changes Password of User
        /// </summary>
        /// <param name="newPasswd"></param>
        /// <param name="oldPasswd"></param>
        /// <returns>If Sucess Returns a Message "Sucessfully Updated Password" </returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="DbException"></exception>
        public async Task<string> ChangePassword(string newPasswd,string oldPasswd)
        {
            var id = _tokenService.GetUidFromToken();
            if (id == null)
            {
                throw new UnauthorizedUserException("Token Invalid");
            }
            var user = await _userRepository.GetById((int)id);
            if(user== null)
            {
                throw new NoSuchItemInDbException("The User with Given Id Doesnt exist");

            }
            var comparePasswd = _hashService.AuthenticatePassword(oldPasswd ,user.Salt, user.Password);
            if (!comparePasswd)
            {
                throw new UnauthorizedAccessException("Please Enter Right Password");
            }
            var (passwd, salt) = _hashService.HashPasswd(newPasswd);
            user.Password = passwd;
            user.Salt = salt;
            user = await _userRepository.Update(user);
            if (user == null) throw new DbException();
            return "Sucessfully Updated Password";

        }
        #endregion

        #region DeleteAddress
        /// <summary>
        /// Delete Address of user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>If Sucess Returns "Sucess" </returns>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="DbException"></exception>
        public async Task<string> DeleteAddress(int id)
        {
            var address = await _addressRepository.GetById(id);
            if(address == null)
            {
                throw new NoSuchItemInDbException("The Address With Given Doesnt exist");

            }
            var uid = _tokenService.GetUidFromToken();
            if(uid != address.CustomerId)
            {
                throw new UnauthorizedAccessException("You Dont Have Previlage to edit this address");
            }
            var res = await _addressRepository.Delete(id);
            if(!res)
            {
                throw new DbException();
            }
            return "Sucess";
        }
        #endregion

        #region EditAddress
        /// <summary>
        /// Edit Address of user
        /// </summary>
        /// <param name="addressDTO"></param>
        /// <returns>AddressDTO</returns>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="DbException"></exception>
        public async Task<AddressDTO> EditAddress(AddressDTO addressDTO)
        {
            int id = addressDTO.Id;
             var address = await _addressRepository.GetById(id);
            
            if (address == null)
            {
                throw new NoSuchItemInDbException("No Such Address With Given Id");
            }
            var userId = _tokenService.GetUidFromToken();
            if (userId != address.CustomerId)
            {
                throw new UnauthorizedAccessException("User Doesnt privallge to edit the address");
            }
            if (addressDTO.PrimaryAdress)
            {
                await _addressRepository.MakePrimaryAddressFalse((int)userId);
            }
            address = _dtoService.MapAddressDTOToAddress(addressDTO , address);
            address  = await _addressRepository.Update(address);
            if(address == null)
            {
                throw new DbException();
            }
            return addressDTO; 
        }
        #endregion

        #region EditProfile
        /// <summary>
        /// Updates Firstname and lastname of user
        /// </summary>
        /// <param name="userProfileEditDTO"></param>
        /// <returns>UserReturnDTO</returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="DbException"></exception>
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
            if(user == null)
            {
                throw new DbException();
            }
            var customer = await _customerRepository.GetById(user.Uid);
            var userReturnDTo = _dtoService.MapUserToUserReturnDTO(customer, "");
            return userReturnDTo;

        }
        #endregion

        #region ViewProfile
        /// <summary>
        /// Fetches Profile Of user
        /// </summary>
        /// <returns>UserReturnDTO</returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoSuchItemInDbException"></exception>
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
        #endregion


        #region ViewAddress
        /// <summary>
        /// Fetches List Of Address's of a user
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UnauthorizedUserException"></exception>
        /// <exception cref="NoItemsInDbException"></exception>
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
        #endregion

    }
}
