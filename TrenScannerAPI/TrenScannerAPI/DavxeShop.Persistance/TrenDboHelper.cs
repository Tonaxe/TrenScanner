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

        public bool GetUser(string user)
        {
            var userExist = _context.Usuarios.Where(u => u.Correo == user).FirstOrDefault();

            return userExist != null;
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
    }
}
