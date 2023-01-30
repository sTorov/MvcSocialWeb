using Microsoft.EntityFrameworkCore.Infrastructure;
using MvcSocialWeb.Data.Repositories.Interfaces;

namespace MvcSocialWeb.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialWebContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(SocialWebContext context)
        {
            _context = context;
        }

        public void Dispose() { }

        public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class
        {
            if(_repositories == null)
                _repositories= new Dictionary<Type, object>();

            if (hasCustomRepository)
            {
                var cusromRepo = _context.GetService<IRepository<TEntity>>();
                if (cusromRepo != null)
                    return cusromRepo;
            }

            var type = typeof(TEntity);
            if(!_repositories.ContainsKey(type))
                _repositories[type] = new Repository<TEntity>(_context);

            return (IRepository<TEntity>)_repositories[type];
        }

        public int SaveChanges(bool ensureAutoHistory = false)
        {
            return default;
        }
    }
}
