#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LolGameReporter.TelegramBot/LolGameReporter.TelegramBot.csproj", "LolGameReporter.TelegramBot/"]
COPY ["LolGameReporter.Core/LolGameReporter.Core.csproj", "LolGameReporter.Core/"]
COPY ["LolGameReporter.Data/LolGameReporter.Data.csproj", "LolGameReporter.Data/"]
COPY ["LolGameReporter.Services/LolGameReporter.Services.csproj", "LolGameReporter.Services/"]
COPY ["LolGameReporter.Utilities/LolGameReporter.Common.csproj", "LolGameReporter.Utilities/"]
RUN dotnet restore "./LolGameReporter.TelegramBot/LolGameReporter.TelegramBot.csproj"
COPY . .
WORKDIR "/src/LolGameReporter.TelegramBot"
RUN dotnet build "./LolGameReporter.TelegramBot.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LolGameReporter.TelegramBot.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LolGameReporter.TelegramBot.dll"]