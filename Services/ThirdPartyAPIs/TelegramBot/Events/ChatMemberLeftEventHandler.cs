using Common;
using Core.Services.Database;
using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Events;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Services.ThirdPartyAPIs.TelegramBot.Events
{
    public class ChatMemberLeftEventHandler(
            ILogger<ChatMemberLeftEventHandler> logger,
            ITelegramBotClientService telegramBotClientService,
            IChatService chatService
        ) : IEventHandler
    {
        public async Task HandleEventAsync(Update update, CancellationToken cts)
        {
            if (update.Message is not { } message) return;

            var botClient = telegramBotClientService.BotClient;

            Chat messageChat = message.Chat;
            var leftChatmember = update.Message.LeftChatMember;

            if (!(leftChatmember != null && leftChatmember.IsBot && leftChatmember.Id == botClient.BotId)) return;
            var from = message.From;

            await chatService.DeleteChatByTelegramChatId(
                messageChat.Id,
                from?.Id ?? 0,
                StringUtilities.GetFullName(from?.FirstName, from?.LastName) ?? string.Empty
            );

            logger.LogInformation("Chat deleted. Chat name is \"{chatName}\"", messageChat.Title);
        }
    }
}
