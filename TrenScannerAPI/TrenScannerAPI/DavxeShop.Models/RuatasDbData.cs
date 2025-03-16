namespace DavxeShop.Models
{
    public class RutasDbData
    {
        public int id_ruta { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public List<ViajesDbData> Viajes { get; set; } = new List<ViajesDbData>();
    }
}
