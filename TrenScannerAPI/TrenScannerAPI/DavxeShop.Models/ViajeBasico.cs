namespace DavxeShop.Models
{
    public class ViajeBasico
    {
        public int id_viaje{ get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string duracion { get; set; }
        public string fecha { get; set; }
        public decimal precio { get; set; }
    }
}
