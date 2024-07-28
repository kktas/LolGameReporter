using Core.Services.Database;
using Core.Services.ThirdPartyAPIs.TelegramBot.Events;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Services.ThirdPartyAPIs.TelegramBot.Events
{
    public class ChatTitleChangedEventHandler(
            ILogger<ChatTitleChangedEventHandler> logger,
            IChatService chatService
        ) : IEventHandler
    {
        public async Task HandleEventAsync(Update update, CancellationToken cts)
        {
            if (update.Message is not { } message) return;

            Chat messageChat = message.Chat;
            string newChatTitle = message.NewChatTitle ?? string.Empty;

            var chatInDb = await chatService.GetChatByTelegramChatId(messageChat.Id);
            var oldChatName = chatInDb.Name;
            var chatUpdated = await chatService.UpdateChatName(messageChat.Id, newChatTitle);

            logger.LogInformation(
                "Chat with id {chatId}: name has been changed from \"{oldChatName}\" to \"{newChatName}\"",
                chatUpdated.Id,
                oldChatName,
                chatUpdated.Name
            );

        }
    }
}
