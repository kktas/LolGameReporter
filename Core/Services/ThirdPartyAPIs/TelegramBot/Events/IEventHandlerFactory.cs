using Telegram.Bot.Types.Enums;

namespace Core.Services.ThirdPartyAPIs.TelegramBot.Events
{
    public interface IEventHandlerFactory
    {
        public IEventHandler GetEventHandler(MessageType messageType);
    }
}

