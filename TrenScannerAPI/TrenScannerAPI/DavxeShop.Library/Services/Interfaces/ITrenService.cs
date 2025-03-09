using DavxeShop.Models;

namespace DavxeShop.Library.Services.Interfaces
{
    public interface ITrenService
    {
        List<TrenDbData> GetRecomendedTrains();
    }
}
