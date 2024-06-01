using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Core.Services.ThirdPartyAPIs.TelegramBot.Events;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Services.ThirdPartyAPIs.TelegramBot
{
    public class TelegramBotUpdateHandler(IEventHandlerFactory eventHandlerFactory, ICommandHandlerFactory commandHandlerFactory) : ITelegramBotUpdateHandler
    {

        public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Message message = update.Message ?? new Message();

            var chatId = message.Chat.Id;

            if (message.Type != MessageType.Text)
            {
                var eventHandler = eventHandlerFactory.GetEventHandler(message.Type);
                await eventHandler.HandleEventAsync(update, cancellationToken);
            }

            if (message.Text is null)
                return;

            var commandEntity = message.Entities?.FirstOrDefault(e => e.Type == MessageEntityType.BotCommand);

            if (commandEntity is not null)
            {
                //// Get Command
                string pattern = @"^/([^@\s]+)";
                Match match = Regex.Match(message.Text, pattern);
                var commandText = match.Groups[1] is not null ? match.Groups[1].ToString() : string.Empty;

                TelegramCommandType[] commandTypes = (TelegramCommandType[])Enum.GetValues(typeof(TelegramCommandType));

                TelegramCommandType commandType = commandTypes.FirstOrDefault(ct => ct.ToString().ToLower().Equals(commandText.ToLower()));

                // Handle Command
                ICommandHandler commandHandler = commandHandlerFactory.GetCommandHandler(commandType);
                await commandHandler.HandleCommandAsync(update, cancellationToken);
            }
        }
    }
}
