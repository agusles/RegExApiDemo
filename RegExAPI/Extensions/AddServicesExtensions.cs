using RegExAPI.Domain.Contracts;
using RegExAPI.Domain.Models;
using RegExAPI.Infrastructure.Services;

namespace RegExAPI.Extensions
{
    public static class AddServicesExtensions
    {
        public static IServiceCollection AddApplicationConfig(
            this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JsonServiceOptions>(
                config.GetSection(JsonServiceOptions.Position));

            return services;
        }

        public static IServiceCollection AddApplicationServices(
             this IServiceCollection services)
        {
            services.AddScoped<IJsonClient, JsonPlaceholderClient>();
            services.AddScoped<IRegExService, RegExService>();

            return services;
        }
    }
}
