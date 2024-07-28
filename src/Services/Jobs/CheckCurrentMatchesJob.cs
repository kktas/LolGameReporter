using Core.Jobs;
using Core.Models;
using Core.Services.Cache;
using Core.Services.Cache.Models;
using Core.Services.Database;
using Core.Services.ThirdPartyAPIs.Riot;
using Core.Services.ThirdPartyAPIs.TelegramBot;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Refit;
using System.Net;
using Telegram.Bot;

namespace Services.Jobs
{
    internal class CheckCurrentMatchesJob(
            IAccountService accountService,
            IRiotApiFactory riotApiFactory,
            IRedisCacheService redisCacheService,
            ILogger<CheckCurrentMatchesJob> logger,
            ITelegramBotClientService telegramBotClientService,
            IServiceScopeFactory scopeFactory
        ) : ICheckCurrentMatchesJob
    {
        public async Task ExecuteAsync()
        {
            try
            {
                IEnumerable<Account> accounts = await accountService.GetAllAccountsWithServerWithChats();
                var tasks = new List<Task>();

                await Parallel.ForEachAsync(accounts, async (account, cancellationToken) => {
                    try
                    {
                        using var scope = scopeFactory.CreateScope();
                        var _championService = scope.ServiceProvider.GetService<IChampionService>() ?? throw new ArgumentNullException("ChampionService is null!");

                        var serverClient = riotApiFactory.CreateServerClient(account.Server.Name);

                        ActiveGame activeGameResponse = await serverClient.GetActiveGames(account.Puuid);

                        var key = $"activegame-{account.Puuid}";


                        ActiveGame lastActiveGame = await redisCacheService.GetAsync<ActiveGame>(key);

                        if (lastActiveGame != null)
                        {
                            if (lastActiveGame.GameId.Equals(activeGameResponse?.GameId)) return;
                        }

                        if (activeGameResponse is null) return;

                        var newGame = await redisCacheService.SetAsync<ActiveGame>(key, activeGameResponse, new DistributedCacheEntryOptions() { AbsoluteExpiration = new DateTime(2199, 12, 31, 23, 59, 59, DateTimeKind.Utc) });

                        var participant = activeGameResponse.Participants.First(p => p.Puuid == account.Puuid);
                        var oldRiotId = $"{account.GameName}#{account.TagLine}";

                        if (!oldRiotId.Equals(participant.RiotId))
                        {
                            var riotIdParts = participant.RiotId.Split('#');
                            string newGameName = riotIdParts[0];
                            string newTagLine = riotIdParts[1];
                            await accountService.ChangeAccountGameNameTagLine(account, newGameName, newTagLine);
                        }

                        var botClient = telegramBotClientService.BotClient;
                        var champion = await _championService.GetByChampionId(participant.ChampionId);

                        var message = $"{account.GameName}#{account.TagLine} is playing {champion.Name}";

                        foreach (var chat in account.Chats)
                        {
                            await botClient.SendTextMessageAsync(
                                chatId: chat.TelegramChatId, 
                                text: message,
                                cancellationToken: cancellationToken
                            );
                        }
                    }
                    catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
                    {
                        return;
                    }
                    catch (ApiException ex)
                    {
                        logger.LogError("API Error! Message: {Message}\nStacktrace: {StackTrace}", ex.Message, ex.StackTrace);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                });
            }
            catch (Exception ex)
            {
                throw;
            }
            return;
        }
    }
}
