using DavxeShop.Library.Services.Interfaces;
using DavxeShop.Models;
using DavxeShop.Persistance.Interfaces;

namespace DavxeShop.Library.Services
{
    public class TrenService : ITrenService
    {
        private readonly ITrenDboHelper _trenDboHelper;

        public TrenService(ITrenDboHelper trenDboHelper)
        {
            _trenDboHelper = trenDboHelper;
        }

        public List<TrenDbData> GetRecomendedTrains()
        {
            var random = new Random();
            var hoy = DateTime.Today.AddDays(1);

            var allTrains = _trenDboHelper.GetAllTrenes()
                .Where(x =>
                {
                    var fechas = x.Fecha.Split('/');
                    if (fechas.Length > 0)
                    {
                        DateTime fechaInicio;
                        if (DateTime.TryParse(fechas[0], out fechaInicio))
                        {
                            return fechaInicio >= hoy;
                        }
                    }
                    return false;
                }).OrderBy(x => random.Next()).Take(10).ToList();

            return allTrains;
        }
    }
}
