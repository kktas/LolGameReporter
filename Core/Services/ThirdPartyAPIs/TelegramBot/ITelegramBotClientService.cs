using Telegram.Bot;

namespace Core.Services.ThirdPartyAPIs.TelegramBot
{
    public interface ITelegramBotClientService
    {
        public TelegramBotClient BotClient { get; }

    }
}
