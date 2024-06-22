using Core.Services.Cache.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.ThirdPartyAPIs.Riot
{
    public interface IServerApi
    {
        [Get("/lol/spectator/v5/active-games/by-summoner/{summonerId}")]
        Task<ActiveGame> GetActiveGames(string summonerId);
    }
}
