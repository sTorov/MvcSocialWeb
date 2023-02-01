using MvcSocialWeb.Data.Repositories;
using MvcSocialWeb.Data.Repositories.Interfaces;

namespace MvcSocialWeb.Middlewares.Extensions
{
    public static class ServerCollectionExtensions
    {
        /// <summary>
        /// Добавление UnitOfWork репозиториев в сервисы
        /// </summary>
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services) 
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        /// <summary>
        /// Добавление кастомного репозитория в сервисы
        /// </summary>
        public static IServiceCollection AddCustomRepository<TEntity, TRepository>(this IServiceCollection services) 
            where TEntity : class
            where TRepository : Repository<TEntity>
        {
            services.AddScoped<IRepository<TEntity>, TRepository>();
            return services;
        }
    }
}
