using Common;
using Core.Services.Database;
using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Events;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Services.ThirdPartyAPIs.TelegramBot.Events
{
    public class ChatMembersAddedEventHandler(
            ILogger<ChatMembersAddedEventHandler> logger,
            ITelegramBotClientService telegramBotClientService,
            IChatService chatService
        ) : IEventHandler
    {
        public async Task HandleEventAsync(Update update, CancellationToken cts)
        {
            if (update.Message is not { } message) return;

            var botClient = telegramBotClientService.BotClient;

            Chat messageChat = message.Chat;

            if (update.Message.NewChatMembers?.FirstOrDefault(m => m.IsBot && m.Id == botClient.BotId) == null) return;

            var from = message.From;

            var chatToBeCreated = new Core.Models.Chat()
            {
                Name = messageChat.Title ?? string.Empty,
                TelegramChatId = messageChat.Id,
                CreatedAt = DateTime.UtcNow,
                CreatedById = from?.Id ?? 0,
                CreatedByName = StringUtilities.GetFullName(from?.FirstName, from?.LastName) ?? string.Empty
            };

            var chatCreated = await chatService.CreateChat(chatToBeCreated);

            logger.LogInformation("Chat created. Chat name is \"{chatName}\"", chatCreated.Name);
        }
    }
}
