
using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Events;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.ThirdPartyAPIs.TelegramBot.Events
{
    public class UnknownEventHandler(ITelegramBotClientService botClientService) : IEventHandler
    {
        //protected readonly ITelegramBotClient _botClient = 0;

        protected readonly ITelegramBotClient _botClient = botClientService.BotClient;

        public virtual Task HandleEventAsync(Update update, CancellationToken cts)
        {
            return Task.CompletedTask;
        }
    }
}
