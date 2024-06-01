using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Services.ThirdPartyAPIs.TelegramBot.Commands.ActiveGames
{
    public class ActiveGamesCommandHandler(ITelegramBotClientService botClientService) : ICommandHandler
    {
        public async Task HandleCommandAsync(Update update, CancellationToken cts)
        {
            var botClient = botClientService.BotClient;

            Message message = update.Message ?? new Message();


            InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(
                new InlineKeyboardButton[][]{
                    // first row
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData(text: "1.1", callbackData: "11"),
                        InlineKeyboardButton.WithCallbackData(text: "1.2", callbackData: "12")
                    },
                    // second row
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData(text: "2.1", callbackData: "21"),
                        InlineKeyboardButton.WithCallbackData(text: "2.2", callbackData: "22")
                    },
                });

            await botClient
                .SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Active Games Command is triggered",
                    replyMarkup: inlineKeyboardMarkup,
                    cancellationToken: cts
            );
        }
    }
}
