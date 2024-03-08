using Microsoft.EntityFrameworkCore;

namespace ExamenAlexanderFuela1.Models
{
    public class Contexto : DbContext
    {
        public Contexto (DbContextOptions<Contexto> options) : base(options)
        {
        }
    }
}
