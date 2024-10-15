using Microsoft.EntityFrameworkCore;
using Sprint4.Models;

namespace Sprint4.Database
{
    public class MongoDbContext : DbContext
    { 
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<PlanoSaude> Planos { get; set; }
        public DbSet<RecomendacaoPlanoSaude> Recomendacoes { get; set; }

        public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options) { 
        
        }
    }
}
