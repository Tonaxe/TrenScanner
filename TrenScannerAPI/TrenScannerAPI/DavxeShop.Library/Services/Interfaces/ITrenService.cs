using DavxeShop.Models;

namespace DavxeShop.Library.Services.Interfaces
{
    public interface ITrenService
    {
        List<ViajeBasico> GetRecomendedTrains();
        List<ViajeSimplificado> GetAllTrains();
        bool DeleteTren(string username, int id_viaje);
        bool UpdateTren(int id, UpdateTren trenInfo);
        UpdateTren GetTrainById(int id);
    }
}
