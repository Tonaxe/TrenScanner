namespace DavxeShop.Models
{
    public class ViajesDbData
    {
        public int id_viaje { get; set; }
        public string salida { get; set; }
        public string llegada { get; set; }
        public string duracion { get; set; }
        public string tipo_transbordo { get; set; }
        public string tanda { get; set; }
        public string fecha { get; set; }
        public int id_ruta { get; set; }
        public RutasDbData Ruta { get; set; }
        public List<TarifasDbData> Tarifas { get; set; } = new List<TarifasDbData>();
    }
}
