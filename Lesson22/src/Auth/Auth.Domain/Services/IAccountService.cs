using Auth.Domain.Entities;

namespace Auth.Domain.Services
{
    /// <summary>
    /// Сервис управления аккаунтами
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Создание аккаунта
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Account> CreateAsync(string login, string password);
        
        /// <summary>
        /// Логин аккаунта
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<Account> LoginAsync(string login, string password);
    }
}