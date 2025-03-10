using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DavxeShop.Library.Services.Interfaces;
using DavxeShop.Models;
using DavxeShop.Persistance;
using DavxeShop.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DavxeShop.Library.Services
{
    public class CSVProcessorService : ICSVProcessorService
    {
        private readonly IDbContextFactory<TrenScannerContext> _contextFactory;
        private readonly ITrenDboHelper _trenDboHelper;

        public CSVProcessorService(IDbContextFactory<TrenScannerContext> contextFactory, ITrenDboHelper trenDboHelper)
        {
            _contextFactory = contextFactory;
            _trenDboHelper = trenDboHelper;
        }

        public async Task ImportarTrenesDesdeCsv(TrenData trenData)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true
            };

            //using (var reader = new StreamReader(@"C:\Users\yassi\Desktop\DAW\TrenScannerS\scraperDatos.csv"))
            using (var reader = new StreamReader(@"C:\Users\Tonaxe\Desktop\TrenScanner\scraperDatos.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();

                var trenes = new List<TrenDbData>();
                int nextTanda = _trenDboHelper.GetNextTanda();

                while (csv.Read())
                {
                    var precioStr = csv.GetField<string>("Precio").Replace(",", ".");
                    decimal precio;
                    try
                    {
                        precio = Math.Round(Convert.ToDecimal(precioStr, CultureInfo.InvariantCulture), 2);
                    }
                    catch (Exception ex)
                    {
                        precio = 0.00m;
                    }

                    var tren = new TrenDbData
                    {
                        Origen = trenData.Origin,
                        Destino = trenData.Destination,
                        Salida = csv.GetField<string>("Salida"),
                        Llegada = csv.GetField<string>("Llegada"),
                        Duracion = csv.GetField<string>("Duración"),
                        Tipo_Transbordo = csv.GetField<string>("Tipo Transbordo"),
                        Tarifa = csv.GetField<string>("Tarifa"),
                        Precio = precio,
                        IdaVuelta = csv.GetField<string>("IdaVuelta").ToLower() == "ida" ? 0 : 1,
                        Tanda = nextTanda,
                        Fecha = trenData.DepartureDate + "/" + trenData.ReturnDate,
                    };

                    trenes.Add(tren);
                }

                await using var context = _contextFactory.CreateDbContext();

                try
                {
                    context.Trenes.AddRange(trenes);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar los trenes: {ex.Message}");
                }
            }
        }
    }
}
