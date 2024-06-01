using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ThirdPartyAPIs.Riot
{
    internal interface IRegionAPI
    {
        [Get("/lol/match/v5/matches/by-puuid/{summonerId}")]
        Task<dynamic> GetPastMatches(int start, int count);

        [Get("/riot/account/v1/accounts/by-riot-id/{gameName}/{tagLine}")]
        Task<dynamic> GetPuuidByRiotName(string gameName, string tagLine);
    }
}
