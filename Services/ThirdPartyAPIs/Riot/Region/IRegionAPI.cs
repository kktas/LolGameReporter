using Core.Services.ThirdPartyAPIs.Riot;
using Newtonsoft.Json.Linq;
using Refit;
using System.Reflection.PortableExecutable;


namespace Services.ThirdPartyAPIs.Riot.Region
{
    public interface IRegionAPI
    {
        [Get("/lol/match/v5/matches/by-puuid/{summonerId}")]
        Task<dynamic> GetPastMatches(string summonerId, int start, int count, [Header("X-Riot-Token")] string token);
        //var headers = new Dictionary<string, string> { { "Authorization", "Bearer tokenGoesHere" }, { "X-Tenant-Id", "123" } };

        [Get("/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}")]
        Task<RiotAccountDTO> GetPuuidByRiotName(string gameName, string tagLine);
    }
}
