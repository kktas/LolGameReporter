using Core.Services.Cache;
using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Core.Services.ThirdPartyAPIs.TelegramBot.Events;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Services.ThirdPartyAPIs.TelegramBot
{
    public class TelegramBotUpdateHandler(
        IEventHandlerFactory eventHandlerFactory,
        ICommandHandlerFactory commandHandlerFactory,
        ILogger<TelegramBotUpdateHandler> logger,
        IRedisCacheService cacheService,
        ITelegramBotUpdateVerifier updateVerifier
    ) : ITelegramBotUpdateHandler
    {

        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message?.Chat.Id ?? update.CallbackQuery?.Message?.Chat.Id;

            if (chatId == null)
            {
                logger.LogError("Chat id is not avaliable for the update: {update}", update);
                return;
            }

            var from = update.Message?.From ?? update.CallbackQuery?.Message?.From ?? throw new("Message or command is from no one");
            Message message = update.Message;

            //handle events
            if (message is not null && message.Type != MessageType.Text)
            {
                var eventHandler = eventHandlerFactory.GetEventHandler(message.Type);
                await eventHandler.HandleEventAsync(update, cancellationToken);

                return;
            }

            try
            {
                await updateVerifier.Verify((long)chatId);
            }
            catch (Exception ex)
            {
                logger.LogError("There is no active chat with telegram chat id: {chatId}", chatId);
                return;

            }

            var activeCommand = await cacheService.GetAsync<CommandData>($"command:user:{from.Id}");
            await cacheService.RemoveAsync($"command:user:{from.Id}");
            var commandEntity = message?.Entities?.FirstOrDefault(e => e.Type == MessageEntityType.BotCommand);
            string commandText = string.Empty;

            // Handle Callback Queries
            if (update.CallbackQuery is not null)
            {
                var data = update.CallbackQuery.Data;

                if (data is null) return;
                var parts = data.Split("/");
                if (parts.Length != 2) return;

                commandText = parts[0];
            }
            // handle commands
            else if (commandEntity is not null && message?.Text is not null)
            {
                //// Get Command
                string pattern = @"^/([^@\s]+)";
                Match match = Regex.Match(message.Text, pattern);
                commandText = match.Groups[1] is not null ? match.Groups[1].ToString() : string.Empty;
            }
            // handle text messages in case they are replies to commands
            else if (message.Text is not null)
            {
                if (activeCommand is null) return;

               commandText = activeCommand.Name;
            }

            if (!String.IsNullOrEmpty(commandText))
            {
                await HandleTelegramCommandUpdateAsync(update,  commandText, cancellationToken, activeCommand);
            }
        }

        private async Task HandleTelegramCommandUpdateAsync(Update update,  string commandName,  CancellationToken cancellationToken, CommandData? commandData = null)
        {
            TelegramCommandType[] commandTypes = (TelegramCommandType[])Enum.GetValues(typeof(TelegramCommandType));
            TelegramCommandType commandType = commandTypes.FirstOrDefault(ct => ct.ToString().ToLower().Equals(commandName));

            ICommandHandler commandHandler = commandHandlerFactory.GetCommandHandler(commandType);
            await commandHandler.HandleCommandAsync(update, cancellationToken, commandData);
        }
    }
}
