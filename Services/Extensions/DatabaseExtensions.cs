using Core;
using Core.Services.Database;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Database;

namespace LolGameReporter.Services.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LolGameReporterDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("Connection"),
                    x =>
                    {
                        x.MigrationsAssembly("Data");
                        x.MigrationsHistoryTable(configuration["Database:MigrationsHistoryTable:Name"], configuration["Database:MigrationsHistoryTable:Schema"]);
                    }
                )
            );

            return services;
        }

        public static IServiceCollection AddDbServices(this IServiceCollection services)
        {
            // Add Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add ServicesAddD
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IChatService, ChatService>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<IServerService, ServerService>();

            return services;
        }

    }
}
