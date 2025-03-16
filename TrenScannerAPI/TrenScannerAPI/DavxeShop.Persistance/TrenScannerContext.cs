using DavxeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace DavxeShop.Persistance
{
    public class TrenScannerContext : DbContext
    {
        public TrenScannerContext(DbContextOptions<TrenScannerContext> options) : base(options) { }

        public DbSet<TrenDbData> Trenes { get; set; }
        public DbSet<TarifasDbData> Tarifas { get; set; }
        public DbSet<RutasDbData> Rutas { get; set; }
        public DbSet<ViajesDbData> Viajes { get; set; }
        public DbSet<UserDbData> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RutasDbData>().HasKey(r => r.id_ruta);
            modelBuilder.Entity<ViajesDbData>().HasKey(v => v.id_viaje);
            modelBuilder.Entity<TarifasDbData>().HasKey(t => t.id_tarifa);
            modelBuilder.Entity<ViajesDbData>().HasOne(v => v.Ruta).WithMany(r => r.Viajes).HasForeignKey(v => v.id_ruta); 
            modelBuilder.Entity<TarifasDbData>().HasOne(t => t.Viaje).WithMany(v => v.Tarifas).HasForeignKey(t => t.id_viaje);
        }
    }
}
