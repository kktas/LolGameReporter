using Common;
using Core.Services.Cache;
using Core.Services.Database;
using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Microsoft.Extensions.Caching.Distributed;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Services.ThirdPartyAPIs.TelegramBot.Commands.AddAccount
{
    public class AddAccountSelectServerCallbackQueryHandler(ITelegramBotClientService botClientService, IRedisCacheService redisCacheService) : ICommandHandler, ICallbackable
    {
        public string CallbackName => "addaccountenteraccountname";

        public async Task HandleCommandAsync(Update update, CancellationToken cts, CommandData? activeCommand)
        {
            var callbackQuery = update.CallbackQuery ?? throw new Exception("Callback query is null");
            var data = callbackQuery?.Data ?? throw new Exception("Datais null");
            var chatId = update.CallbackQuery.Message?.Chat.Id ?? throw new Exception("The message is from an unknown chat");
            var from = callbackQuery?.From.Id ?? throw new Exception("The message is from no one");

            var serverId = Convert.ToInt32(data.Split("/")[1]);

            TelegramBotClient botClient = botClientService.BotClient;

            string commandKey = $"command:user:{from}";
            CommandData commandData = new(CallbackName, new() { { "serverId", serverId } });

            await redisCacheService.SetAsync<CommandData>(commandKey, commandData, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) });

            if (update.CallbackQuery?.Message is null) throw new(nameof(update.CallbackQuery.Message));

            await botClient
                .EditMessageTextAsync(
                    chatId: chatId,
                    messageId: update.CallbackQuery.Message.MessageId,
                    text: $"Enter username along with its tagline (e.g. KKT#TR1)",
                    cancellationToken: cts
            );
        }
    }
}
