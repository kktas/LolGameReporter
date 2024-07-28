using Common;
using Core.Models;
using Core.Services.Database;
using Core.Services.ThirdPartyAPIs.Riot;
using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Refit;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.ThirdPartyAPIs.TelegramBot.Commands.AddAccount
{
    public class AddAccountEnterAccountNameCallbackQueryHandler(ITelegramBotClientService botClientService, IAccountService accountService, IChatService chatService, IRiotApiFactory riotApiFactory) : ICommandHandler
    {
        public async Task HandleCommandAsync(Update update, CancellationToken cts, CommandData? commandData)
        {
            Message message = update.Message ?? throw new Exception("Message is null");
            var botClient = botClientService.BotClient;

            User from = update.Message.From ?? throw new Exception("Message is from no one");

            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            var messageText = message.Text;

            var textParts = messageText?.Split("#") ?? throw new Exception("Message has no text");

            if (textParts.Length != 2)
            {
                await botClient
                   .SendTextMessageAsync(
                       chatId: chatId,
                       text: $"Invalid input: {messageText}",
                       cancellationToken: cts
                );
                return;
            }

            RiotAccountDTO? user;

            try
            {
                // always use americas because it's the fastest and most reliable
                var americasClient = riotApiFactory.CreateRegionClient("Americas");
                user = await americasClient.GetPuuidByRiotName(textParts[0], textParts[1]);
            }
            catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                user = null;
            }
            catch (ApiException ex)
            {
                // Handle other API errors
                throw new Exception($"API Error: {ex.StatusCode} - {ex.Content}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception($"Exception occurred: {ex.Message}");
            }

            if (user == null)
            {
                await botClient
                   .SendTextMessageAsync(
                       chatId: message.Chat.Id,
                       text: $"User doesn't exist: {message.Text}",
                       cancellationToken: cts
                );
                return;
            }

            var accountToBeAdded = new Account()
            {
                Chats = [await chatService.GetChatByTelegramChatId(chatId)],
                Puuid = user.Puuid,
                GameName = user.GameName,
                ServerId = Convert.ToInt32(commandData?.Data?["serverId"]),
                TagLine = user.TagLine,
                CreatedById = from.Id,
                CreatedByName = StringUtilities.GetFullName(from.FirstName, from.LastName)
            };

            await accountService.CreateAccount(accountToBeAdded);

            await botClient
                .SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Account is added: {accountToBeAdded.GameName}#{accountToBeAdded.TagLine}",
                    cancellationToken: cts
            );
        }
    }
}
