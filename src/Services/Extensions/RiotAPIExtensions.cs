//#pragma warning disable CS8604 // Possible null reference argument.
using Core.Services.ThirdPartyAPIs.Riot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.ThirdPartyAPIs.Riot;

namespace LolGameReporter.Services.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRiotAPIClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddSingleton<IRiotApiFactory, RiotApiFactory>();

            return services;
        }
    }
}

//#pragma warning restore CS8604 // Possible null reference argument.