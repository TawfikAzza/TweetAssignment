﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TweetService/TweetService.csproj", "TweetService/"]
RUN dotnet restore "TweetService/TweetService.csproj"
COPY . .
WORKDIR "/src/TweetService"
RUN dotnet build "TweetService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TweetService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TweetService.dll"]
