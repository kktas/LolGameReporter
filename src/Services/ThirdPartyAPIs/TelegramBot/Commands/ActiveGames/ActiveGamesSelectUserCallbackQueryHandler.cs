using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Services.ThirdPartyAPIs.TelegramBot.Commands.ActiveGames
{
    public class ActiveGamesSelectUserCallbackQueryHandler(ITelegramBotClientService botClientService) : ICommandHandler
    {
        public async Task HandleCommandAsync(Update update, CancellationToken cts, CommandData _)
        {
            var botClient = botClientService.BotClient;

            Message message = update.Message ?? new Message();


            //InlineKeyboardMarkup inlineKeyboardMarkup = new InlineKeyboardMarkup(
            //    new InlineKeyboardButton[][]{
            //        // first row
            //        new InlineKeyboardButton[]
            //        {
            //            InlineKeyboardButton.WithCallbackData(text: "user1", callbackData: "selectuser/user1"),
            //            InlineKeyboardButton.WithCallbackData(text: "user2", callbackData: "selectuser/user2")
            //        },
            //        // second row
            //        new InlineKeyboardButton[]
            //        {
            //            InlineKeyboardButton.WithCallbackData(text: "user3", callbackData: "selectuser/user3"),
            //            InlineKeyboardButton.WithCallbackData(text: "user4", callbackData: "selectuser/user4")
            //        },
            //    });

            if (update.CallbackQuery?.Message is null) throw new ArgumentNullException(nameof(update.CallbackQuery.Message));

            await botClient
                .EditMessageTextAsync(
                    messageId: update.CallbackQuery.Message.MessageId,
                    chatId: update.CallbackQuery.Message.Chat.Id,
                    text: $"Select user reply is triggered",
                    //replyMarkup: inlineKeyboardMarkup,
                    cancellationToken: cts
            );
        }
    }
}
