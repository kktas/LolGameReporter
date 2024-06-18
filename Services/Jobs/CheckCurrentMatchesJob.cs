using Core.Jobs;
using Core.Services.Cache;
using Core.Services.Cache.Models;
using Core.Services.ThirdPartyAPIs.TelegramBot;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Services.ThirdPartyAPIs.Riot.Server;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Telegram.Bot;

namespace Services.Jobs
{
    internal class CheckCurrentMatchesJob(
            ITRAPI trapi,
            IRedisCacheService redisCacheService,
            ILogger<CheckCurrentMatchesJob> logger,
            ITelegramBotClientService telegramBotClientService
        ) : ICheckCurrentMatchesJob
    {
        public async Task ExecuteAsync()
        {
            try
            {
                ActiveGame activeGameResponse = await trapi.GetActiveGames("QaeZmcSJasIDuD0JVrnT1W53j91pfZHRWSfMc1wfI2FLdIML1YtzQ-bDrv4nf9LWH0Gla573BM9u6g");

                var key = $"activegame-{1}";

                ActiveGame lastActiveGame = await redisCacheService.GetAsync<ActiveGame>(key);

                if (lastActiveGame != null)
                {
                    if (lastActiveGame.GameId.Equals(activeGameResponse?.GameId)) return;
                }

                if (activeGameResponse is null) return;

                var newGame = await redisCacheService.SetAsync<ActiveGame>(key, activeGameResponse, new DistributedCacheEntryOptions() { AbsoluteExpiration = new DateTime(2199, 12, 31, 23, 59, 59, DateTimeKind.Utc) });

                var botClient = telegramBotClientService.BotClient;

                await botClient.SendTextMessageAsync(
                    chatId: -4221875162,
                    text: "User started new game"
                );

            }
            catch (Exception ex)
            {
                var a = 1;
            }
            return;

        }
    }
}
