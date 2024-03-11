FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ProfileService/ProfileService.csproj", "ProfileService/"]
RUN dotnet restore "ProfileService/ProfileService.csproj"
COPY . .
WORKDIR "/src/ProfileService"
RUN dotnet build "ProfileService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfileService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /src/ProfileService/wait-for-it.sh . 
# Make sure to copy the script to the final image
RUN chmod +x wait-for-it.sh

# Use the script to wait for RabbitMQ before starting your service
ENTRYPOINT ["./wait-for-it.sh", "rabbitmq:5672", "--", "dotnet", "ProfileService.dll"]