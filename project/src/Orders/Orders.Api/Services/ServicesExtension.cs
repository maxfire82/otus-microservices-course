using Common.Authentication.Services;
using Common.Authentication.Services.Implementation;
using Orders.Api.HostedServices;

namespace Orders.Api.Services
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddHostedService<OrderProcessingService>();
            services.AddHostedService<KafkaProducerService>();
            services.AddHostedService<KafkaConsumerService>();
            //services.AddHostedService<KafkaConsumer2Service>();
            
            return services;
        }
    }
}