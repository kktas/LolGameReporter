using Core.Services.ThirdPartyAPIs.TelegramBot;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Services.ThirdPartyAPIs.TelegramBot;
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
        IServiceScopeFactory serviceScopeFactory
    ) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken _cancellationToken)
        {
            Console.WriteLine("execute async");

            var botClient = _telegramBotClientService.BotClient;
            using CancellationTokenSource cts = new();

            //jobManager.Configure(serviceScopeFactory);

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
                using IServiceScope scope = serviceScopeFactory.CreateScope();
                ITelegramBotUpdateHandler telegramBotUpdateHandler = scope.ServiceProvider.GetService<ITelegramBotUpdateHandler>() ?? throw new Exception("No update handler has been found!");
                await telegramBotUpdateHandler.HandleAsync(botClient, update, cancellationToken);
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