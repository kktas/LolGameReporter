using System.Data;
using Telegram.Bot.Types;

namespace Core.Services.ThirdPartyAPIs.TelegramBot.Commands
{
    public interface ICommandHandlerFactory
    {
        public ICommandHandler GetCommandHandler(TelegramCommandType commandType);
    }
}

