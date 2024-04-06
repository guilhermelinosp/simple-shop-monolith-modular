FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/shop-services-notifications/shop-services-notifications.api/shop-services-notifications.api.csproj", "Source/shop-services-notifications/shop-services-notifications.api/"]
RUN dotnet restore "Source/shop-services-notifications/shop-services-notifications.api/shop-services-notifications.api.csproj"
COPY . .
WORKDIR "/src/Source/shop-services-notifications/shop-services-notifications.api"
RUN dotnet build "shop-services-notifications.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "shop-services-notifications.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shop-services-notifications.api.dll"]
