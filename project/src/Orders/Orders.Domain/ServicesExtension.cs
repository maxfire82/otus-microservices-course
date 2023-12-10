using Common.Idempotency.Services;
using Common.Idempotency.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Internal;
using Orders.Domain.Services;
using Orders.Domain.Services.Implementation;

namespace Orders.Domain
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
        public static IServiceCollection AddDomainServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<OrderDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IOrderService, OrderService>();
            services.TryAddSingleton<ISystemClock, SystemClock>();
            services.AddScoped<IIdempotencyService, IdempotencyService>();

            return services;
        }
    }
}