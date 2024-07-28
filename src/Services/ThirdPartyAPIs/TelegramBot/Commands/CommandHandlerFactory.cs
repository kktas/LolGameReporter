using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Microsoft.Extensions.DependencyInjection;
using Services.ThirdPartyAPIs.TelegramBot.Commands.ActiveGames;
using Services.ThirdPartyAPIs.TelegramBot.Commands.AddAccount;

namespace Services.ThirdPartyAPIs.TelegramBot.Commands
{
    public class CommandHandlerFactory(IServiceProvider serviceProvider) : ICommandHandlerFactory
    {
        public ICommandHandler GetCommandHandler(TelegramCommandType commandType)
        {

            return commandType switch
            {
                TelegramCommandType.ActiveGames => serviceProvider.GetRequiredService<ActiveGamesCommandHandler>(),
                TelegramCommandType.ActiveGamesSelectUser => serviceProvider.GetRequiredService<ActiveGamesSelectUserCallbackQueryHandler>(),
                TelegramCommandType.AddAccount => serviceProvider.GetRequiredService<AddAccountCommandHandler>(),
                TelegramCommandType.AddAccountSelectServer => serviceProvider.GetRequiredService<AddAccountSelectServerCallbackQueryHandler>(),
                TelegramCommandType.AddAccountEnterAccountName => serviceProvider.GetRequiredService<AddAccountEnterAccountNameCallbackQueryHandler>(),
                _ => serviceProvider.GetRequiredService<UnknownCommandHandler>()
            };
        }
    }
}