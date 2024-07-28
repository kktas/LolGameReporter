using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Services.ThirdPartyAPIs.TelegramBot.Commands.ActiveGames
{
    public class ActiveGamesCommandHandler(ITelegramBotClientService botClientService) : ICommandHandler, ICallbackable
    {
        public string CallbackName => "activegameselectuser";

        public async Task HandleCommandAsync(Update update, CancellationToken cts, CommandData _)
        {
            var botClient = botClientService.BotClient;

            Message message = update.Message ?? new Message();


            InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(
                new InlineKeyboardButton[][]{
                    // first row
                       new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData(text: "user1", callbackData: "selectuser/KKT#TR1"),
                        InlineKeyboardButton.WithCallbackData(text: "user2", callbackData: "selectuser/KKT2#TR1"),
                        //InlineKeyboardButton.WithCallbackData(text: "user2", callbackData: "selectuser/user2")
                    },
                    // second row
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData(text: "user3", callbackData: "selectuser/user3"),
                        InlineKeyboardButton.WithCallbackData(text: "user4", callbackData: "selectuser/user4"),
                        InlineKeyboardButton.WithCallbackData(text: "user5", callbackData: "selectuser/user5")
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
