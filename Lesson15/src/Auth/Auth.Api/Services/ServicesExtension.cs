using Common.Authentication.Services;
using Common.Authentication.Services.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Services
{
    /// <summary>
    /// Класс инициализации сервисов
    /// </summary>
    public static class ServicesExtension
    {
        /// <summary>
        /// Инициализации сервисов
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        { 
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}