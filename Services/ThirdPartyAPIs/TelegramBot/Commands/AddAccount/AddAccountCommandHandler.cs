using Common;
using Core.Models;
using Core.Services.Cache;
using Core.Services.Database;
using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Services.ThirdPartyAPIs.TelegramBot.Commands.AddAccount
{
    public class AddAccountCommandHandler(ITelegramBotClientService botClientService, IRedisCacheService redisCacheService, IServerService serverService) : ICommandHandler, ICallbackable
    {
        public string CallbackName => "addaccountselectserver";

        public async Task HandleCommandAsync(Update update, CancellationToken cts, CommandData _)
        {
            Message message = update.Message ?? new Message();
            TelegramBotClient botClient = botClientService.BotClient;

            long from = update.Message?.From?.Id ?? throw new Exception("from is empty");

            IEnumerable<Server> servers = await serverService.GetAllServers();
            List<InlineKeyboardButtonDTO> buttons = servers.Select(s => new InlineKeyboardButtonDTO(s.Name, $"{CallbackName}/{s.Id}")).ToList();

            InlineKeyboardMarkup markup = TelegramInlineMarkupUtils.CreateInlineKeyboardMarkup(buttons, 3);

            await botClient
                .SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: $"Select the server of the account",
                    replyMarkup: markup,
                    cancellationToken: cts
            );
        }
    }
}
