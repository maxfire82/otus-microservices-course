namespace Common.Authentication.Services;

public interface IIdentityService
{
    /// <summary>
    /// Получает ID аккаунта
    /// </summary>
    /// <returns></returns>
    long GetAccountId();
}