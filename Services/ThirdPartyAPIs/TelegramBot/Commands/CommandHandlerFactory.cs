using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Microsoft.Extensions.DependencyInjection;
using Services.ThirdPartyAPIs.TelegramBot.Commands.ActiveGames;

namespace Services.ThirdPartyAPIs.TelegramBot.Commands
{
    public class CommandHandlerFactory(IServiceProvider serviceProvider) : ICommandHandlerFactory
    {
        public ICommandHandler GetCommandHandler(TelegramCommandType commandType)
        {

            return commandType switch
            {
                TelegramCommandType.ActiveGames => serviceProvider.GetRequiredService<ActiveGamesCommandHandler>(),
                _ => serviceProvider.GetRequiredService<UnknownCommandHandler>()
            };
        }
    }
}