using DavxeShop.Models;

namespace DavxeShop.Persistance.Interfaces
{
    public interface ITrenDboHelper
    {
        int GetNextTanda();
        UserDbData GetUser(string user);
        UserDbData GetUserDb(string user);
        public void SaveChanges();
        bool AddUser(UserData userData);
        List<ViajesDbData> GetAllTrenes();
        List<ViajeSimplificado> GetAllTrenes2();
        ViajesDbData GetTrenById(int id_viaje);
        void DeleteTren(int id_viaje);
    }
}
