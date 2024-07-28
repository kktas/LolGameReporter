using Telegram.Bot;
using Telegram.Bot.Types;

namespace Core.Services.ThirdPartyAPIs.TelegramBot
{
    public interface ITelegramBotUpdateHandler
    {
        public Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
    }
}
