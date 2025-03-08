using DavxeShop.Models;

namespace DavxeShop.Library.Services.Interfaces
{
    public interface IUserService
    {
        bool RegisterUser(UserData userData);
        bool GetUser(string user);
        string GenerateJwtToken(string user);
        void SaveToken(string user, string token);
    }
}
