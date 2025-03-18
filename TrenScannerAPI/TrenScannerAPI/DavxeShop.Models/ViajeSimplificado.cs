namespace DavxeShop.Models
{
    public class ViajeSimplificado
    {
        public int IdViaje { get; set; }
        public string Salida { get; set; }
        public string Llegada { get; set; }
        public string Duracion { get; set; }
        public string TipoTransbordo { get; set; }
        public string Tanda { get; set; }
        public string Fecha { get; set; }
        public int IdRuta { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Tarifa { get; set; }
        public decimal Precio { get; set; }
    }
}
