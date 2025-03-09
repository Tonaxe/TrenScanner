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

        public List<TrenDbData> GetAllTrenes()
        {
            return _context.Trenes.ToList();
        }
    }
}
