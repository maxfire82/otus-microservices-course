using Otus.Users.Models;

namespace Otus.Users.Services
{
    /// <summary>
    /// Сервис управления задачами
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Создание задачи
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<User> CreateAsync(User item);

        /// <summary>
        /// Получение задачи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetAsync(long id);

        /// <summary>
        /// Обновление задачи
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpdateAsync(User item);

        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveAsync(long id);
    }
}