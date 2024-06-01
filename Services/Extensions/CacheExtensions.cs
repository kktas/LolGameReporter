using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace LolGameReporter.Services.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetConnectionString("Redis"));

            return services;
        }

        public static IServiceCollection AddRedLockService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDistributedLockFactory, RedLockFactory>(sp =>
            {
                var redisConnectionString = configuration.GetConnectionString("Redis");

                if (redisConnectionString is null)
                    throw new ArgumentNullException(nameof(redisConnectionString));

                var multiplexers = new List<RedLockMultiplexer>
                    {
                        ConnectionMultiplexer.Connect(redisConnectionString)
                    };
                return RedLockFactory.Create(multiplexers);
            });
            return services;
        }
    }
}
