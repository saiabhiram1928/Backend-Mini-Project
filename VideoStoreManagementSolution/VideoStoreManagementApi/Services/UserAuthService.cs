using VideoStoreManagementApi.Exceptions;
using VideoStoreManagementApi.Interfaces.Repositories;
using VideoStoreManagementApi.Interfaces.Services;
using VideoStoreManagementApi.Models;
using VideoStoreManagementApi.Models.DTO;

namespace VideoStoreManagementApi.Services
{
    public class UserAuthService :IUserAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashService _hashService;
        private readonly IDTOService _dtoService;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITokenService _tokenService;

        #region Constructor
        public UserAuthService(IUserRepository userRepository , IHashService hashService, IDTOService dtoService ,ICustomerRepository customerRepository , ITokenService tokenService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _dtoService = dtoService;
            _customerRepository = customerRepository;
            _tokenService = tokenService;
        }
        #endregion

        #region Login
        /// <summary>
        /// Validates User Credentials and login user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>UserReturnDTO</returns>
        /// <exception cref="NoSuchItemInDbException"></exception>
        /// <exception cref="UnauthorizedUserException"></exception>
        public async Task<UserReturnDTO> Login(UserLoginDTO userDTO)
        {
            var user = await _userRepository.GetUserByEmail(userDTO.Email);
            if(user == null)
            {
                throw new NoSuchItemInDbException("User doenst Exist");
            }
            bool ComparePasswd = _hashService.AuthenticatePassword(userDTO.Password, user.Salt, user.Password);
            if (!ComparePasswd)
            {
                throw new UnauthorizedUserException();
            }
           var customer = await _customerRepository.GetById(user.Uid);
            var token  = _tokenService.GenerateToken(user);
            
            var userReturnDTO = _dtoService.MapUserToUserReturnDTO(customer, token);
            return userReturnDTO;
        }
        #endregion

        #region Register
        /// <summary>
        /// Register user and validates credentials
        /// </summary>
        /// <param name="userRegisterDTO"></param>
        /// <returns>UserReturnDTO</returns>
        /// <exception cref="DuplicateItemException"></exception>
        /// <exception cref="DbException"></exception>
        public async Task<UserReturnDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            var userCheck = await _userRepository.CheckUserExist(userRegisterDTO.Email);
            if (userCheck)
            {
                throw new DuplicateItemException("User Already Exist , Please Login");
            }
            var (passwd,salt) = _hashService.HashPasswd(userRegisterDTO.Password); 
            var user = _dtoService.MapUserRegisterDTOToUser(userRegisterDTO,salt,passwd);
            user =await _userRepository.Add(user);
            if (user == null) throw new DbException();
            var customer  = _dtoService.MapUserRegisterDTOToCustomer(userRegisterDTO,user);
            customer = await _customerRepository.Add(customer);
            if (customer == null)
            {
                await _userRepository.Delete(user.Uid);
                throw new DbException();
            }
            var token = _tokenService.GenerateToken(user);
            var userReturnDTO = _dtoService.MapUserToUserReturnDTO(customer,token);
            return userReturnDTO;
        }
        #endregion
    }
}
