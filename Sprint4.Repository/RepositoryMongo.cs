using Microsoft.EntityFrameworkCore;
using Sprint4.Database;
using Sprint4.Repository.Interface;

namespace Sprint4.Repository
{
    public class MongoDbRepository<T>(MongoDbContext mongoDbContext) : IRepository<T> where T : class
    {
        private readonly MongoDbContext _mongoDBContext = mongoDbContext;
        private readonly DbSet<T> _dbSet;
        public void Add(T entity)
        {
            _mongoDBContext.Add(entity);

            _mongoDBContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _mongoDBContext.Set<T>().ToList();
        }

        public T GetById(string? id)
        {
            return _mongoDBContext.Set<T>().Find(id);
        }

        public T GetById(int? id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

    }
}
