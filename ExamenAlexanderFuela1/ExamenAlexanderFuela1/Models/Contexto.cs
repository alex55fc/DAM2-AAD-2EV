using Microsoft.EntityFrameworkCore;
using static ExamenAlexanderFuela1.Controllers.ProductoController;

namespace ExamenAlexanderFuela1.Models
{
    public class Contexto : DbContext
    {
        public Contexto (DbContextOptions<Contexto> options) : base(options)
        {
        }
        public DbSet<ClienteModelo> Cliente { get; set; }
        public DbSet<ProveedorModelo> Proveedor { get; set; }
        public DbSet<ProductoModelo> Producto { get; set; }
        public DbSet<ComprasModelo> Compras { get; set; }
        public DbSet<VentasModelo> Ventas { get; set; }
        //-------------------------------------------------------------
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductoTotal>().HasNoKey();
        }
        public DbSet<ProductoTotal> vistaTotal { get; set; }

    }
}
