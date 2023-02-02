namespace MvcSocialWeb.Data.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс, обязываюший реализовать основные операции над данными типа <typeparamref name="T"/>
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получение всех обектов <typeparamref name="T"/>
        /// </summary>
        Task<List<T>> GetAllAsync();
        /// <summary>
        /// Получение объекта <typeparamref name="T"/>
        /// </summary>
        Task<T?> GetAsync(int id);
        /// <summary>
        /// Создание объекта <typeparamref name="T"/>
        /// </summary>
        Task CreateAsync(T item);
        /// <summary>
        /// ОБновление объекта <typeparamref name="T"/>
        /// </summary>
        Task UpdateAsync(T item);
        /// <summary>
        /// Удаление объекта <typeparamref name="T"/>
        /// </summary>
        Task DeleteAsync(T item);
    }
}
