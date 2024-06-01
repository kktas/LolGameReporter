using Core.Services.ThirdPartyAPIs.TelegramBot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace TelegramBot
{
    public class Bot(
        ITelegramBotClientService _telegramBotClientService,
        ILogger<Bot> logger,
        IServiceProvider serviceProvider,
        ITelegramBotUpdateHandler telegramBotUpdateHandler
    ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken _cancellationToken)
        {
            var botClient = _telegramBotClientService.BotClient;
            using CancellationTokenSource cts = new();

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = [UpdateType.Message, UpdateType.CallbackQuery] // receive all update types except ChatMember related updates
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync(cancellationToken: _cancellationToken);
            logger.LogInformation("Start listening for {botName}", me.Username);
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                using IServiceScope scope = serviceProvider.CreateScope();

                await telegramBotUpdateHandler.HandleAsync(botClient, update, cancellationToken);

                //if (update.Type == UpdateType.CallbackQuery)
                //{
                //    var a = await botClient
                //        .EditMessageTextAsync(
                //            chatId: update.CallbackQuery.Message.Chat.Id,
                //            messageId: update.CallbackQuery.Message.MessageId,
                //            text: $"kek",
                //            cancellationToken: cancellationToken
                //    );
                //    return;
                //}
                scope.Dispose();
            }
            catch (Exception ex)
            {
                logger.LogError("ERROR!! Message: {Message} \n StackTrace: {StackTrace} ", ex.Message, ex.StackTrace);
            }
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}