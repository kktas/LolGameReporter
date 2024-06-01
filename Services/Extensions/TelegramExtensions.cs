﻿using Core;
using Core.Services.Database;
using Core.Services.ThirdPartyAPIs.TelegramBot;
using Core.Services.ThirdPartyAPIs.TelegramBot.Commands;
using Core.Services.ThirdPartyAPIs.TelegramBot.Events;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using Services.ThirdPartyAPIs.TelegramBot;
using Services.ThirdPartyAPIs.TelegramBot.Commands;
using Services.ThirdPartyAPIs.TelegramBot.Commands.ActiveGames;
using Services.ThirdPartyAPIs.TelegramBot.Events;

namespace LolGameReporter.Services.Extensions
{
    public static partial class ServiceCollectionExtensions
    {

        public static IServiceCollection AddTelegramBotClient(this IServiceCollection services)
        {
            services.AddSingleton<ITelegramBotClientService, TelegramBotClientService>();

            return services;
        }

        public static IServiceCollection AddTelegramUpdateHandler(this IServiceCollection services)
        {
            services.AddScoped<ITelegramBotUpdateHandler, TelegramBotUpdateHandler>();

            return services;
        }

        public static IServiceCollection AddTelegramCommandHandlers(this IServiceCollection services)
        {
            // Add Command Handlers
            services.AddTransient<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddTransient<ActiveGamesCommandHandler>();
            services.AddTransient<UnknownCommandHandler>();

            return services;
        }

        public static IServiceCollection AddTelegramEventHandlers(this IServiceCollection services)
        {
            // Add Event Handlers
            services.AddTransient<IEventHandlerFactory, EventHandlerFactory>();
            services.AddTransient<ChatMembersAddedEventHandler>();
            services.AddTransient<ChatMemberLeftEventHandler>();
            services.AddTransient<ChatTitleChangedEventHandler>();
            services.AddTransient<GroupCreatedEventHandler>();
            services.AddTransient<UnknownEventHandler>();

            return services;
        }

    }
}
