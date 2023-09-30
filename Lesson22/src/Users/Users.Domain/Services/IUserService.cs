namespace Users.Domain.Services
{
    /// <summary>
    /// Сервис управления профилем пользователя
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Создание профиля пользователя
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Models.User> CreateAsync(Models.User item);

        /// <summary>
        /// Получение профиля
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Models.User> GetAsync(long id);

        /// <summary>
        /// Обновление профиля
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpdateAsync(Models.User item);

        /// <summary>
        /// Удаление профиля
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveAsync(long id);
    }
}