using Core.Jobs;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Jobs;
using Services.Jobs.ScopeActivator;
using System.Runtime.CompilerServices;

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
                               options.UseNpgsqlConnection(configuration.GetConnectionString("HangfireConnection"))
                           )
                       );

            //// Add the processing server as IHostedService
            services.AddHangfireServer();
            return services;
        }

        public static IServiceCollection AddJobServices(this IServiceCollection services)
        {

            services.AddScoped<CheckCurrentMatchesJob>();
            return services;
        }
    }

    public static partial class HostExtensions
    {
        public static IHost AddJobActivatorScope(this IHost host)
        {

            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(host.Services.GetService<IServiceScopeFactory>()));

            // Add the processing server as IHostedService
            return host;
        }

        public static IHost AddJobs(this IHost host)
        {
            host.Services.GetService<IRecurringJobManager>()
                .AddOrUpdate<CheckCurrentMatchesJob>
                (
                    "checkcurrentmatchesjob",
                    (a) => a.ExecuteAsync(),
                    Cron.Minutely()
                );

            return host;
        }
    }


}
