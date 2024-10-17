using Microsoft.EntityFrameworkCore;
using Sprint4.Models;

namespace Sprint4.Database
{
    public class MongoDbContext : DbContext
    { 
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<PlanoSaude> Planos { get; set; }
        public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options) { 
        
        }
    }
}
