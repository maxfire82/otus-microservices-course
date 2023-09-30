using Microsoft.Extensions.Configuration;

namespace Common.Configuration;

public static class ConfigurationExtension
{
    /// <summary>
    /// Получает значение по ключу, или выкидывает исключение если значение отсутствует.
    /// </summary>
    /// <param name="configuration">Конфигурация приложения.</param>
    /// <param name="key">Название ключа.</param>
    /// <typeparam name="T">Тип значения.</typeparam>
    /// <returns> Значение.</returns>
    /// <exception>Исключение, выбрасываемое если значение отсутствует.</exception>
    public static T GetValueOrThrow<T>(this IConfiguration configuration, string key)
    {
        var value = configuration.GetValue<T>(key);

        if (value == null)
            throw new System.Exception($"'{key}' не установлен.");

        return value;
    }
}
