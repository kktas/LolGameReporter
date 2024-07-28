using Refit;


namespace Core.Services.ThirdPartyAPIs.Riot
{
    public interface IRegionApi
    {
        [Get("/lol/match/v5/matches/by-puuid/{summonerId}")]
        Task<dynamic> GetPastMatches(string summonerId, int start, int count, [Header("X-Riot-Token")] string token);

        [Get("/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}")]
        Task<RiotAccountDTO> GetPuuidByRiotName(string gameName, string tagLine);
    }
}
