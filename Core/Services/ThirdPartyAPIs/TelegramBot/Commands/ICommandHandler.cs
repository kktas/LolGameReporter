using Telegram.Bot.Types;

namespace Core.Services.ThirdPartyAPIs.TelegramBot.Commands
{
    public interface ICommandHandler
    {
        public Task HandleCommandAsync(Update update, CancellationToken cts, CommandData? commandData = null);
    }
}

