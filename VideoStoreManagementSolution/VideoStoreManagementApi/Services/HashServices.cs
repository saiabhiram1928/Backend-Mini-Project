using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using VideoStoreManagementApi.Interfaces.Services;

namespace VideoStoreManagementApi.Services
{
    public class HashServices:IHashService
    {
        #region AuthenticatePassword
        /// <summary>
        /// Hashes the given text and validates with real password , to authenticate user
        /// </summary>
        /// <param name="enteredpasswd"></param>
        /// <param name="salt"></param>
        /// <param name="realpasswd"></param>
        /// <returns>Return true , if password matches</returns>
        public bool AuthenticatePassword(string enteredpasswd, byte[] salt , byte[] realpasswd)
        {
            HMACSHA512 hmac = new HMACSHA512(salt);
            var encryptpasswd = hmac.ComputeHash(Encoding.UTF8.GetBytes(enteredpasswd));
            if (realpasswd.Length != encryptpasswd.Length) return false;
            for(int i =0; i<realpasswd.Length; i++)
            {
                if (realpasswd[i] != encryptpasswd[i])
                    return false;
            }
            return true;
            
        }
        #endregion

        #region HashPasswd
        /// <summary>
        /// Hashes the given the text
        /// </summary>
        /// <param name="passwd"></param>
        /// <returns>Return byte array of password, salt</returns>
        public (byte[] , byte[]) HashPasswd(string passwd)
        {
            HMACSHA512 hmac = new HMACSHA512();
            var salt = hmac.Key;
            var enypasswd = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwd));
            return (enypasswd , salt);
        }
        #endregion

    }
}
