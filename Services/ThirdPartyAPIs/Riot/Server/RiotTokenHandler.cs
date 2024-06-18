using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ThirdPartyAPIs.Riot.Server
{
    internal class RiotTokenHandler(string riotToken) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("X-Riot-Token", $"{riotToken}");
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
