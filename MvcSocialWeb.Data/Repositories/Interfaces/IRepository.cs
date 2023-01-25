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
        IEnumerable<T> GetAll();
        /// <summary>
        /// Получение объекта <typeparamref name="T"/>
        /// </summary>
        T Get(int id);
        /// <summary>
        /// Создание объекта <typeparamref name="T"/>
        /// </summary>
        void Create(T item);
        /// <summary>
        /// ОБновление объекта <typeparamref name="T"/>
        /// </summary>
        void Update(T item);
        /// <summary>
        /// Удаление объекта <typeparamref name="T"/>
        /// </summary>
        void Delete(int id);
    }
}
