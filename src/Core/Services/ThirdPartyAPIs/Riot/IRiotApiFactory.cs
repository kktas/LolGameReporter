using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.ThirdPartyAPIs.Riot
{
    public interface IRiotApiFactory
    {
        public IRegionApi CreateRegionClient(string region);
        public IServerApi CreateServerClient(string server);
    }
}
