using Telegram.Bot.Types;

namespace Core.Services.ThirdPartyAPIs.TelegramBot.Events
{
    public interface IEventHandler
    {
        public Task HandleEventAsync(Update update, CancellationToken cts);
    }
}
