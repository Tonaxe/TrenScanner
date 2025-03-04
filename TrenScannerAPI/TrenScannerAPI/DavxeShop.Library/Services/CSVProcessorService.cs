using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DavxeShop.Library.Services.Interfaces;
using DavxeShop.Models;
using DavxeShop.Persistance;

namespace DavxeShop.Library.Services
{
    public class CSVProcessorService : ICSVProcessorService
    {
        private readonly TrenScannerContext _context;
        private readonly TrenDboHelper _trenDboHelper;

        public CSVProcessorService(TrenScannerContext context, TrenDboHelper trenDboHelper)
        {
            _context = context;
            _trenDboHelper = trenDboHelper;

        }

        public async Task ImportarTrenesDesdeCsv()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true
            };

            using (var reader = new StreamReader(@"C:\Users\yassi\Desktop\DAW\TrenScannerS\scraperDatos.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();

                var trenes = new List<TrenDbData>();

                int nextTanda = await _trenDboHelper.GetNextTandaAsync();

                while (csv.Read())
                {
                    var tren = new TrenDbData
                    {
                        Salida = csv.GetField<string>("Salida"),
                        Llegada = csv.GetField<string>("Llegada"),
                        Duracion = csv.GetField<string>("Duración"),
                        Tipo_Transbordo = csv.GetField<string>("Tipo Transbordo"),
                        Tarifa = csv.GetField<string>("Tarifa"),
                        Precio = (float)Convert.ToDecimal(csv.GetField<string>("Precio").Replace(".", ","), CultureInfo.InvariantCulture),
                        IdaVuelta = csv.GetField<string>("IdaVuelta").ToLower() == "ida" ? 0 : 1,
                        Tanda = nextTanda
                    };

                    trenes.Add(tren);
                }

                _context.Trenes.AddRange(trenes);
                await _context.SaveChangesAsync();
            }
        }
    }
}