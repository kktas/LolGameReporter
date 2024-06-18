#pragma warning disable CS8604 // Possible null reference argument.
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Services.ThirdPartyAPIs.Riot.Region;
using Services.ThirdPartyAPIs.Riot.Server;

namespace LolGameReporter.Services.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRiotAPIClients(this IServiceCollection services, IConfiguration configuration)
        {
            // Riot Token
            services.AddTransient(sp => new RiotTokenHandler(configuration["Riot:X-Riot-Token"]));

            // server APIs
            services.AddRefitClient<INAAPI>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Riot:Server:NA:BaseUri"]))
                .AddHttpMessageHandler(sp => sp.GetRequiredService<RiotTokenHandler>());

            services.AddRefitClient<IEUWAPI>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Riot:Server:EUW:BaseUri"]))
                .AddHttpMessageHandler(sp => sp.GetRequiredService<RiotTokenHandler>());

            services.AddRefitClient<IEUNEAPI>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Riot:Server:EUNE:BaseUri"]))
                .AddHttpMessageHandler(sp => sp.GetRequiredService<RiotTokenHandler>());

            services.AddRefitClient<ITRAPI>()
               .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Riot:Server:TR:BaseUri"]))
               .AddHttpMessageHandler(sp => sp.GetRequiredService<RiotTokenHandler>());

            services.AddRefitClient<IJPAPI>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Riot:Server:JP:BaseUri"]))
                .AddHttpMessageHandler(sp => sp.GetRequiredService<RiotTokenHandler>());

            // region APIs
            services.AddRefitClient<IAmericasAPI>()
              .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Riot:Region:Americas:BaseUri"]))
              .AddHttpMessageHandler(sp => sp.GetRequiredService<RiotTokenHandler>());

            services.AddRefitClient<IEuropeAPI>()
              .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Riot:Region:Europe:BaseUri"]))
              .AddHttpMessageHandler(sp => sp.GetRequiredService<RiotTokenHandler>());

            services.AddRefitClient<IAsiaAPI>()
              .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Riot:Region:Asia:BaseUri"]))
              .AddHttpMessageHandler(sp => sp.GetRequiredService<RiotTokenHandler>());

            return services;
        }
    }
}

#pragma warning restore CS8604 // Possible null reference argument.