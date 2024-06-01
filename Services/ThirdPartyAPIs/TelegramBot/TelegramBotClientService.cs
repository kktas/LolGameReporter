using Core.Services.ThirdPartyAPIs.TelegramBot;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;

namespace Services.ThirdPartyAPIs.TelegramBot
{
    public class TelegramBotClientService(IConfiguration configuration) : ITelegramBotClientService
    {
        public TelegramBotClient BotClient => new(configuration.GetSection("Telegram")["BotAPIKey"]);
    }
}
