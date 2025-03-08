namespace DavxeShop.Models
{
    public class UserDbData
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Token { get; set; }
        public int Rol { get; set; }
    }
}
