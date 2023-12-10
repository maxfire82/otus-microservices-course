using System.Text;
using Common.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Common.Authentication
{
    /// <summary>
    /// Класс инициализации сервисов домена
    /// </summary>
    public static class ServicesExtension
    {
        /// <summary>
        /// Инициализация сервиса аутентификации
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AuthService(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = config.GetValueOrThrow<string>("JWT:Issuer"),
 
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudiences = new []
                        {
                            "GENERAL",
                            config.GetValueOrThrow<string>("JWT:Audience")
                        },
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetValueOrThrow<string>("JWT_SECRET_KEY"))),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", 
                    new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireClaim("scope", "GENERAL")
                        .RequireAuthenticatedUser().Build()
                    );
                
                auth.DefaultPolicy = auth.GetPolicy("Bearer");
            });
            
            return services;
        }
    }
}