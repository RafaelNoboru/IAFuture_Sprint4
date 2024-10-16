using Microsoft.EntityFrameworkCore;
using Sprint4.Models;

namespace Sprint4.Database
{
    public class OracleDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PlanoSaude> Planos { get; set; }
        public DbSet<RecomendacaoPlanoSaude> Recomendacoes { get; set; }
        
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options)
        {

        }
    }
}
