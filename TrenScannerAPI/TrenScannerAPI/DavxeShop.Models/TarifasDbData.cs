namespace DavxeShop.Models
{
    public class TarifasDbData
    {
        public int id_tarifa { get; set; }
        public int id_viaje { get; set; }
        public ViajesDbData Viaje { get; set; }
        public string tarifa { get; set; }
        public decimal precio { get; set; }
        public string ida_vuelta { get; set; }
    }
}
