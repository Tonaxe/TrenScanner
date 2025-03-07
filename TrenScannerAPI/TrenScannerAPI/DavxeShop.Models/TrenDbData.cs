namespace DavxeShop.Models
{
    public class TrenDbData
    {
        public int Id { get; set; }
        public string Salida { get; set; }
        public string Llegada { get; set; }
        public string Duracion { get; set; }
        public string Tipo_Transbordo { get; set; }
        public string Tarifa { get; set; }
        public float Precio { get; set; }
        public int IdaVuelta { get; set; }
        public int Tanda { get; set; }
        public string Fecha { get; set; }
    }
}
