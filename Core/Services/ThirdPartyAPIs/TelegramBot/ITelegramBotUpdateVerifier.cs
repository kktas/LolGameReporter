using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.ThirdPartyAPIs.TelegramBot
{
    public interface ITelegramBotUpdateVerifier
    {
        public Task Verify(long telegramChatId);
    }
}
