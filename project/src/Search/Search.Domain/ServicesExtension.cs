using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Search.Domain.Services;
using Search.Domain.Services.Implementation;

namespace Search.Domain
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
            services.AddScoped<ISearchService, SearchService>();
            services.TryAddSingleton<IAnnouncementStore, AnnouncementStore>();
            
            return services;
        }
    }
}