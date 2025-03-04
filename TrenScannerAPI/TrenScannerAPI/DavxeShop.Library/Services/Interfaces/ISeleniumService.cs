using DavxeShop.Models;

namespace DavxeShop.Library.Services.Interfaces
{
    public interface ISeleniumService
    {
        string GenerateSeleniumScript(TrenData trenData);
    }
}
