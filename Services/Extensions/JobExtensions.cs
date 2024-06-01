using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.PostgreSql.Factories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LolGameReporter.Services.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHangfireService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(_configuration => _configuration
                           .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                           .UseSimpleAssemblyNameTypeSerializer()
                           .UseRecommendedSerializerSettings()
                           .UsePostgreSqlStorage(options =>
                               options.UseNpgsqlConnection(configuration.GetConnectionString("Connection"))
                           )
                       );

            // Add the processing server as IHostedService
            services.AddHangfireServer();
            return services;
        }
    }
}
