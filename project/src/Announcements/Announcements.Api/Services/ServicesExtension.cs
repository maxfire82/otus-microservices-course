using Announcements.Api.HostedServices;
using Announcements.Domain.Models;
using Common.Authentication.Services;
using Common.Authentication.Services.Implementation;
using Common.Queue.Services;
using Common.Queue.Services.Implementation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Announcements.Api.Services
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
            services.TryAddSingleton<IBackgroundQueue<AnnouncementDTO>, BackgroundQueue<AnnouncementDTO>>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddHostedService<AnnouncementProducerService>();
            
            return services;
        }
    }
}