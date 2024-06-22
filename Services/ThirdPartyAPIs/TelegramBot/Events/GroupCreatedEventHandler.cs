using Common;
using Core.Services.Database;
using Core.Services.ThirdPartyAPIs.TelegramBot.Events;
using Microsoft.Extensions.Logging;
using Telegram.Bot.Types;

namespace Services.ThirdPartyAPIs.TelegramBot.Events
{
    public class GroupCreatedEventHandler(
            ILogger<GroupCreatedEventHandler> logger,
            IChatService chatService
        ) : IEventHandler
    {
        public async Task HandleEventAsync(Update update, CancellationToken cts)
        {
            if (update.Message is not { } message) return;

            Chat messageChat = message.Chat;

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

            //await _botClient.SendTextMessageAsync(
            //               chatId: update.Message?.Chat.Id ?? 0,
            //               text: $"Active Games Command is triggered ()",
            //               cancellationToken: cts
            //           );
        }
    }
}
