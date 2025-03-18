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

        public async Task<List<CsvData>> ImportarTrenesDesdeCsv(TrenData trenData)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true
            };

            var csvDataList = new List<CsvData>();
            //using (var reader = new StreamReader(@"C:\Users\Tonaxe\Desktop\TrenScanner\scraperDatos.csv"))
            using (var reader = new StreamReader(@"C:\Users\yassi\Desktop\DAW\TrenScannerS\scraperDatos.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();

                var viajes = new List<ViajesDbData>();
                var tarifas = new List<TarifasDbData>();
                int nextTanda = _trenDboHelper.GetNextTanda();

                await using var context = _contextFactory.CreateDbContext();

                while (csv.Read())
                {
                    var precioStr = csv.GetField<string>("Precio").Replace(",", ".");
                    decimal precio;
                    try
                    {
                        precio = Math.Round(Convert.ToDecimal(precioStr, CultureInfo.InvariantCulture), 2);
                    }
                    catch (Exception)
                    {
                        precio = 0.00m;
                    }

                    var data = new CsvData
                    {
                        Salida = csv.GetField<string>("Salida"),
                        Llegada = csv.GetField<string>("Llegada"),
                        Duracion = csv.GetField<string>("Duración"),
                        TipoTransbordo = csv.GetField<string>("Tipo Transbordo"),
                        Tarifa = csv.GetField<string>("Tarifa"),
                        Precio = precio,
                        IdaVuelta = csv.GetField<string>("IdaVuelta")
                    };

                    csvDataList.Add(data);

                    try
                    {
                        var ruta = context.Rutas.FirstOrDefault(r => r.origen == trenData.Origin && r.destino == trenData.Destination);

                        if (ruta == null)
                        {
                            ruta = new RutasDbData
                            {
                                origen = trenData.Origin,
                                destino = trenData.Destination
                            };

                            context.Rutas.Add(ruta);
                            await context.SaveChangesAsync();
                        }

                        var viaje = new ViajesDbData
                        {
                            salida = csv.GetField<string>("Salida"),
                            llegada = csv.GetField<string>("Llegada"),
                            duracion = csv.GetField<string>("Duración"),
                            tipo_transbordo = csv.GetField<string>("Tipo Transbordo"),
                            tanda = nextTanda.ToString(),
                            fecha = trenData.DepartureDate + "/" + trenData.ReturnDate,
                            id_ruta = ruta.id_ruta
                        };

                        context.Viajes.Add(viaje);
                        await context.SaveChangesAsync();

                        var tarifa = new TarifasDbData
                        {
                            id_viaje = viaje.id_viaje,
                            tarifa = csv.GetField<string>("Tarifa"),
                            precio = precio,
                            ida_vuelta = csv.GetField<string>("IdaVuelta").ToLower() == "ida" ? 0.ToString() : 1.ToString(),
                        };

                        tarifas.Add(tarifa);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al procesar los datos para la ruta {trenData.Origin},  {trenData.Destination}: {ex.Message}");
                    }
                }

                try
                {
                    context.Tarifas.AddRange(tarifas);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar los datos: {ex.Message}");
                }

                return csvDataList;
            }
        }
    }
}
