using DavxeShop.Models;

namespace DavxeShop.Library.Services.Interfaces
{
    public interface ICSVProcessorService
    {
        Task ImportarTrenesDesdeCsv(TrenData trenData);
    }
}
