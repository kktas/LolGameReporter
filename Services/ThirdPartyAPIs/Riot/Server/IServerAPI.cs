using Core.Services.Cache.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ThirdPartyAPIs.Riot.Server
{
    public interface IServerAPI
    {
        [Get("/lol/spectator/v5/active-games/by-summoner/{summonerId}")]
        Task<ActiveGame> GetActiveGames(string summonerId);
    }
}
