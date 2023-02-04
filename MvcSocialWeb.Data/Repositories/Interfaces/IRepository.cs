namespace MvcSocialWeb.Data.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс, обязываюший реализовать основные операции над данными типа <typeparamref name="T"/>
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получение всех обектов
        /// </summary>
        Task<List<T>> GetAllAsync();
        /// <summary>
        /// Получение объекта
        /// </summary>
        Task<T?> GetAsync(int id);
        /// <summary>
        /// Создание объекта
        /// </summary>
        Task CreateAsync(T item);
        /// <summary>
        /// ОБновление объекта
        /// </summary>
        Task UpdateAsync(T item);
        /// <summary>
        /// Удаление объекта
        /// </summary>
        Task DeleteAsync(T item);
    }
}
