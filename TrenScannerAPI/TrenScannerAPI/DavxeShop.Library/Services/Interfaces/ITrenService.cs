using DavxeShop.Models;

namespace DavxeShop.Library.Services.Interfaces
{
    public interface ITrenService
    {
        List<ViajeBasico> GetRecomendedTrains();
        List<ViajeSimplificado> GetAllTrains();
    }
}
