using API.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<DatabaseSettings>(
            _config.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddScoped<IApiHelper, ApiHelper>();
            services.AddScoped<ITokenHelper, TokenHelper>();

            return services;
        }
    }
}