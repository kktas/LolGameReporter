using Core.Models;
using Core.Services.ThirdPartyAPIs.Riot;
using Microsoft.Extensions.Configuration;
using Refit;

namespace Services.ThirdPartyAPIs.Riot
{
    internal class RiotApiFactory(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IRiotApiFactory
    {
        private readonly string _riotToken = configuration["Riot:X-Riot-Token"];
        public IRegionApi CreateRegionClient(string region)
        {
            string configKey = $"Riot:Region:{region}:BaseUri";
            string baseUri = configuration[configKey]
                ?? throw new ArgumentNullException(configKey);

            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(baseUri);
            httpClient.DefaultRequestHeaders.Add("X-Riot-Token", _riotToken);
            return RestService.For<IRegionApi>(httpClient);
        }

        public IServerApi CreateServerClient(string server)
        {
            string configKey = $"Riot:Server:{server}:BaseUri";
            string baseUri = configuration[configKey]
                ?? throw new ArgumentNullException(configKey);

            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(baseUri);
            httpClient.DefaultRequestHeaders.Add("X-Riot-Token", _riotToken);
            return RestService.For<IServerApi>(httpClient);
        }
    }
}
