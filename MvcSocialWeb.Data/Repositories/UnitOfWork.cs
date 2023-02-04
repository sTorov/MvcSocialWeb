using Microsoft.EntityFrameworkCore.Infrastructure;
using MvcSocialWeb.Data.Repositories.Interfaces;

namespace MvcSocialWeb.Data.Repositories
{
    /// <summary>
    /// Класс для получения любого репозитория, зарегистрированного в приложении
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialWebContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(SocialWebContext context)
        {
            _context = context;
        }

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
            return _context.SaveChanges(ensureAutoHistory);
        }


        private bool isDisposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
                return;

            if(disposing)
                _context.Dispose();

            isDisposed = true;
        }
    }
}
