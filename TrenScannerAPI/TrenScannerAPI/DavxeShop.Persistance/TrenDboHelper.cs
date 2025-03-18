using DavxeShop.Models;
using DavxeShop.Persistance.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DavxeShop.Persistance
{
    public class TrenDboHelper : ITrenDboHelper
    {
        private readonly TrenScannerContext _context;

        public TrenDboHelper(TrenScannerContext context)
        {
            _context = context;
        }

        public int GetNextTanda()
        {
            var maxTanda = _context.Trenes
                .Max(t => (int?)t.Tanda) ?? 0;

            return maxTanda == 0 ? 1 : maxTanda + 1;
        }

        public UserDbData GetUser(string user)
        {
            var userExist = _context.Usuarios.Where(u => u.Correo == user).FirstOrDefault();

            return userExist;
        }

        public UserDbData GetUserDb(string user)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Correo == user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }


        public bool AddUser(UserData userData)
        {
            try
            {
                var existingUser = _context.Usuarios.Where(u => u.Correo == userData.Correo).FirstOrDefault();

                if (existingUser != null)
                {
                    return false;
                }

                var newUser = new UserDbData
                {
                    Nombre = userData.Nombre,
                    Correo = userData.Correo,
                    Contraseña = userData.Contraseña,
                    Token = string.Empty,
                    Rol = 0
                };

                _context.Usuarios.Add(newUser);
                _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ViajesDbData> GetAllTrenes()
        {
            return _context.Viajes.Include(v => v.Ruta).Include(v => v.Tarifas).ToList();
        }

        public List<ViajeSimplificado> GetAllTrenes2()
        {
            var allTrains = _context.Viajes
                .Include(v => v.Ruta)
                .Include(v => v.Tarifas)
                .Select(v => new ViajeSimplificado
                {
                    IdViaje = v.id_viaje,
                    Salida = v.salida,
                    Llegada = v.llegada,
                    Duracion = v.duracion,
                    TipoTransbordo = v.tipo_transbordo,
                    Tanda = v.tanda,
                    Fecha = v.fecha,
                    IdRuta = v.Ruta.id_ruta,
                    Origen = v.Ruta.origen,
                    Destino = v.Ruta.destino,
                    Tarifa = v.Tarifas.FirstOrDefault().tarifa,
                    Precio = v.Tarifas.FirstOrDefault().precio
                }).ToList();

            return allTrains;
        }

        public UpdateTren GetTrainById(int id)
        {
            Console.WriteLine($"Buscando tren con ID: {id}");

            var getTrainById = _context.Viajes
                .Include(v => v.Ruta)
                .Include(v => v.Tarifas)
                .Where(v => v.id_viaje == id)
                .Select(v => new UpdateTren
                {
                    Salida = v.salida,
                    Llegada = v.llegada,
                    Duracion = v.duracion,
                    TipoTransbordo = v.tipo_transbordo,
                    Fecha = v.fecha,
                    Origen = v.Ruta.origen,
                    Destino = v.Ruta.destino,
                    Tarifa = v.Tarifas.Any() ? v.Tarifas.First().tarifa : null,
                    Precio = v.Tarifas.Any() ? v.Tarifas.First().precio : 0
                })
                .FirstOrDefault();

            if (getTrainById == null)
            {
                Console.WriteLine("No se encontró el tren.");
            }
            else
            {
                Console.WriteLine("Tren encontrado.");
            }

            return getTrainById;
        }


        public ViajesDbData GetTrenById(int id_viaje)
        {
            return _context.Viajes
                .Include(v => v.Ruta)
                .Include(v => v.Tarifas)
                .FirstOrDefault(v => v.id_viaje == id_viaje);
        }

        public void DeleteTren(int id_viaje)
        {
            var tren = _context.Viajes.FirstOrDefault(v => v.id_viaje == id_viaje);

            _context.Viajes.Remove(tren);
            _context.SaveChanges();
        }

        public bool UpdateTren(int id, UpdateTren trenInfo)
        {
            var rutaExistente = _context.Rutas
                .FirstOrDefault(r => r.origen == trenInfo.Origen && r.destino == trenInfo.Destino);

            int idRuta;
            if (rutaExistente != null)
            {
                idRuta = rutaExistente.id_ruta;
            }
            else
            {
                var nuevaRuta = new RutasDbData
                {
                    origen = trenInfo.Origen,
                    destino = trenInfo.Destino
                };

                _context.Rutas.Add(nuevaRuta);
                _context.SaveChanges();
                idRuta = nuevaRuta.id_ruta;
            }

            var tren = _context.Viajes
                .Include(v => v.Ruta)
                .Include(v => v.Tarifas)
                .FirstOrDefault(v => v.id_viaje == id);

            if (tren == null)
            {
                return false;
            }

            tren.salida = trenInfo.Salida;
            tren.llegada = trenInfo.Llegada;
            tren.duracion = trenInfo.Duracion;
            tren.tipo_transbordo = trenInfo.TipoTransbordo;
            tren.tanda = GetNextTanda().ToString();
            tren.fecha = trenInfo.Fecha;
            tren.id_ruta = idRuta;

            if (tren.Tarifas != null && tren.Tarifas.Any())
            {
                var tarifa = tren.Tarifas.FirstOrDefault();
                if (tarifa != null)
                {
                    tarifa.tarifa = trenInfo.Tarifa;
                    tarifa.precio = trenInfo.Precio;
                }
            }

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
