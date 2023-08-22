using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Otus.Users.Services;
using Otus.Users.Services.Implementation;

namespace Otus.Users
{
    /// <summary>
    /// Класс инициализации сервисов домена
    /// </summary>
    public static class ServicesExtension
    {
        /// <summary>
        /// Инициализации сервисов
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IServiceCollection AddUserServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UserDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}