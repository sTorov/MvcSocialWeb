using Microsoft.EntityFrameworkCore;
using MvcSocialWeb.Data.Repositories.Interfaces;

namespace MvcSocialWeb.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext _dbContext;
        public DbSet<T> Set { get; private set; }

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            var set = _dbContext.Set<T>();
            set.Load();

            Set = set;
        }

        public void Create(T item)
        {
            Set.Add(item);
            _dbContext.SaveChanges();
        }

        public void Delete(T item)
        {
            Set.Remove(item);
            _dbContext.SaveChanges();
        }

        public T? Get(int id) => Set.Find(id);

        public IEnumerable<T> GetAll() => Set;

        public void Update(T item)
        {
            Set.Update(item);
            _dbContext.SaveChanges();
        }
    }
}
