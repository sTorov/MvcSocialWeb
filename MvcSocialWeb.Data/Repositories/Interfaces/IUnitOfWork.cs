namespace MvcSocialWeb.Data.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс для получение любого реализованного репозитория
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Сохранение изменений в базе данных
        /// </summary>
        int SaveChanges(bool ensureAutoHistory = false);
        /// <summary>
        /// Получение интерфейса репозитория IRepository для объектов <typeparamref name="TEntity"/>
        /// </summary>
        IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = true) where TEntity : class;    }
}
