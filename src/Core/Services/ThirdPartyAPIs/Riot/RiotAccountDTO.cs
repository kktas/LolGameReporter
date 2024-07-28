using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.ThirdPartyAPIs.Riot
{
    public record RiotAccountDTO(string GameName, string TagLine, string Puuid);
}
