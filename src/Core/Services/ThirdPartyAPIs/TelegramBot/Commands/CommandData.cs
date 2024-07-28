using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.ThirdPartyAPIs.TelegramBot.Commands
{
    public record CommandData(string Name, Dictionary<string, object>? Data = null);
}
