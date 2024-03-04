FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Source/shop-services-gateway/shop-services-gateway.api/shop-services-gateway.api.csproj", "Source/shop-services-gateway/shop-services-gateway.api/"]
RUN dotnet restore "Source/shop-services-gateway/shop-services-gateway.api/shop-services-gateway.api.csproj"
COPY . .
WORKDIR "/src/Source/shop-services-gateway/shop-services-gateway.api"
RUN dotnet build "shop-services-gateway.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "shop-services-gateway.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shop-services-gateway.api.dll"]
