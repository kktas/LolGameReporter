using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.ThirdPartyAPIs.TelegramBot.Commands
{
    public interface ICallbackable
    {
        public string CallbackName { get; }
    }
}
