using LolGameReporter.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBot;

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
                .AddTelegramUpdateHandlers()
                .AddTelegramCommandHandlers()
                .AddTelegramEventHandlers()
                .AddRiotAPIClients(configuration)
                .AddRedisService(configuration)
                .AddRedLockService(configuration)
                .AddHangfireService(configuration)
                .AddHostedService<Bot>();
        });

        var app = builder.Build();

        app.AddJobActivatorScope()
            .AddJobs();

        app.Run();
    }
}