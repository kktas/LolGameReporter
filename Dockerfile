# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file and restore dependencies
COPY *.sln .
COPY LolGameReporter.Core/*.csproj ./LolGameReporter.Core/
COPY LolGameReporter.Data/*.csproj ./LolGameReporter.Data/
COPY LolGameReporter.Services/*.csproj ./LolGameReporter.Services/
COPY LolGameReporter.TelegramBot/*.csproj ./LolGameReporter.TelegramBot/
RUN dotnet nuget locals --clear all

# Copy the remaining source code and build the application
COPY . .
RUN dotnet build -c Release --restore

# Stage 2: Create the runtime image
# Stage 2: Runtime
FROM ubuntu:22.04

# Set environment variables
ENV DEBIAN_FRONTEND=noninteractive

# Install dependencies
RUN apt-get update && \
    apt-get install -y wget apt-transport-https && \
    wget https://packages.microsoft.com/config/ubuntu/24.04/packages-microsoft-prod.deb && \
    dpkg -i packages-microsoft-prod.deb && \
    apt-get update && \
    apt-get install -y dotnet-runtime-8.0 && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*
    
WORKDIR /app

# Copy the built application from the build stage
COPY --from=build /app/LolGameReporter.Core/bin/Release/net8.0 ./LolGameReporter.Core/
COPY --from=build /app/LolGameReporter.Services/bin/Release/net8.0 ./LolGameReporter.Data/
COPY --from=build /app/LolGameReporter.Services/bin/Release/net8.0 ./LolGameReporter.Services/
COPY --from=build /app/LolGameReporter.TelegramBot/bin/Release/net8.0 ./LolGameReporter.TelegramBot/
#COPY --from=build /app/kaankerimtas@gmail.com_eu-central-1_free-tier.pem .

RUN  apt update

#RUN apt install openssh-server -y
#RUN systemctl status ssh
#RUN systemctl start ssh
#RUN systemctl enable ssh
#RUN systemctl enable ssh --now
#RUN ufw status
#RUN apt-get install ssh
#RUN systemctl enable ssh --now
#RUN systemctl start ssh
#
## Get ssh credentials
#RUN ssh -i kaankerimtas@gmail.com_eu-central-1_free-tier.pem -L 5432:localhost:5435 ubuntu@ec2-3-77-192-142.eu-central-1.compute.amazonaws.com
#
# Set the startup project
ENTRYPOINT ["dotnet", "LolGameReporter.TelegramBot/LolGameReporter.TelegramBot.dll"]