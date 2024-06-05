namespace VideoStoreManagementApi.Interfaces.Services
{
    public interface IGeoLocationServices
    {
        Task<double> GetDistanceAsync(string origin, string destination);
    }
}
