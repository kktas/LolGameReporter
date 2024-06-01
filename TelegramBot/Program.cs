// See https://aka.ms/new-console-template for more information
using Hangfire;
using LolGameReporter.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBot;
using TelegramBot.Jobs;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        builder.ConfigureServices((context, services) =>
        {
            IConfiguration configuration = context.Configuration;
            services.AddSingleton(configuration);

            services
                .AddSingleton(configuration)
                .AddDbConnection(configuration)
                .AddDbServices()
                .AddTelegramBotClient()
                .AddTelegramUpdateHandler()
                .AddTelegramCommandHandlers()
                .AddTelegramEventHandlers()
                .AddRedisService(configuration)
                .AddRedLockService(configuration)
                .AddHangfireService(configuration)
                .AddHostedService<BotService>();
        });

        var app = builder.Build();

        app.Services.GetService<IRecurringJobManager>().AddOrUpdate("myrecurringjob",
             () => Job1.Test(),
              Cron.Minutely());

        app.Run();
    }
}