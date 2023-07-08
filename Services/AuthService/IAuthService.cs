using BikeStoresApi.Models;

namespace BikeStoresApi.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<long>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
