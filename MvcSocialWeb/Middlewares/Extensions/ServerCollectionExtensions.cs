using MvcSocialWeb.Data.Repositories;
using MvcSocialWeb.Data.Repositories.Interfaces;
using MvcSocialWeb.Middlewares.Services;

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

        /// <summary>
        /// Добавление сервиса с общим функционалом для контроллеров
        /// </summary>
        public static IServiceCollection AddUserServices(this IServiceCollection services) 
        {
            services.AddScoped<UserServices>();
            return services;
        }

        /// <summary>
        /// Добавление сервиса генерации тестовых пользователей
        /// </summary>
        public static IServiceCollection AddUserGeneration(this IServiceCollection services)
        {
            services.AddScoped<UserGeneration>();
            return services;
        }
    }
}
