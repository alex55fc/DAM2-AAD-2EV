using Microsoft.EntityFrameworkCore;
using static MVC2024.Controllers.VehiculoController;

namespace MVC2024.Models
{
	public class Contexto : DbContext
	{
        public Contexto(DbContextOptions<Contexto> options) : base (options)
        {
            
            
        }
        //el dbset es como un enumerable o una lista de objetos de tipo MarcaModelo
		public DbSet<MarcaModelo> Marcas { get; set; }
        public DbSet<SerieModelo> Series { get; set; }
        public DbSet<VehiculoModelo> Vehiculo { get; set;}

        //-------------------------------------------------------------
        //ejercicio.Crear una clase para almacenar los datos de los vehiculos de la consulta de SQL Management
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehiculoTotal>().HasNoKey();
        }
        public DbSet<VehiculoTotal> vistaTotal { get; set; }
	}
}
