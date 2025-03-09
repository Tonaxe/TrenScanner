using DavxeShop.Models;

namespace DavxeShop.Persistance.Interfaces
{
    public interface ITrenDboHelper
    {
        int GetNextTanda();
        bool GetUser(string user);
        UserDbData GetUserDb(string user);
        public void SaveChanges();
        bool AddUser(UserData userData);
        List<TrenDbData> GetAllTrenes();
    }
}
