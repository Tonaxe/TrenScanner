using DavxeShop.Models;

namespace DavxeShop.Library.Services.Interfaces
{
    public interface ICSVProcessorService
    {
        Task<List<CsvData>> ImportarTrenesDesdeCsv(TrenData trenData);
    }
}
