using Search.Api.HostedServices;

namespace Search.Api.Services
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
            services.AddHostedService<AnnouncementConsumerService>();
            
            return services;
        }
    }
}