using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Services.ThirdPartyAPIs.TelegramBot.Commands
{
    public class UnknownCommandHandler(ITelegramBotClientService botClientService) : ICommandHandler
    {

        public async virtual Task HandleCommandAsync(Update update, CancellationToken cts, CommandData _)
        {
            var botClient = botClientService.BotClient;
            var chatId = update.Message?.Chat.Id;

            if (chatId == null) return;

            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Command Not Found!",
                cancellationToken: cts
            );
        }
    }
}
