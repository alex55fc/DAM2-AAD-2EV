using Microsoft.EntityFrameworkCore;

namespace ParaExamen2.Models
{
	public class Contexto : DbContext
	{
		public Contexto(DbContextOptions<Contexto> options) : base(options)
		{

		}
		// esto es para que se cree la tabla en la base de datos
		public DbSet<CursoModelo> Cursos { get; set; }

	
	}
}
