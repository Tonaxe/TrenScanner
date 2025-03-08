using DavxeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace DavxeShop.Persistance
{
    public class TrenScannerContext : DbContext
    {
        public TrenScannerContext(DbContextOptions<TrenScannerContext> options) : base(options) { }

        public DbSet<TrenDbData> Trenes { get; set; }
        public DbSet<UserDbData> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
