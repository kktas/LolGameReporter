using Core.Services.ThirdPartyAPIs.TelegramBot.Events;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.Enums;

namespace Services.ThirdPartyAPIs.TelegramBot.Events
{
    public class EventHandlerFactory(IServiceProvider serviceProvider) : IEventHandlerFactory
    {
        public IEventHandler GetEventHandler(MessageType messageType)
        {
            return messageType switch
            {
                MessageType.ChatMembersAdded => serviceProvider.GetRequiredService<ChatMembersAddedEventHandler>(),
                MessageType.ChatMemberLeft => serviceProvider.GetRequiredService<ChatMemberLeftEventHandler>(),
                MessageType.ChatTitleChanged => serviceProvider.GetRequiredService<ChatTitleChangedEventHandler>(),
                MessageType.GroupCreated => serviceProvider.GetRequiredService<GroupCreatedEventHandler>(),
                _ => serviceProvider.GetRequiredService<UnknownEventHandler>()
            };
        }
    }
}