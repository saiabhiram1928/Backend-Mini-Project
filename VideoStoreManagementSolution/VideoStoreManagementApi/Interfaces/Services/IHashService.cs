namespace VideoStoreManagementApi.Interfaces.Services
{
    public interface IHashService
    {
        public bool AuthenticatePassword(string enteredpasswd, byte[] salt, byte[] realpasswd);
        public (byte[], byte[]) HashPasswd(string passwd);
    }
}
