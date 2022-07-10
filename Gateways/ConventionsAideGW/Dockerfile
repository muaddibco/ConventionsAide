#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateways/ConventionsAideGW/ConventionsAideGW.csproj", "Gateways/ConventionsAideGW/"]
COPY ["Core/Core.Services/Core.Services.csproj", "Core/Core.Services/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Logging/Core.Logging.csproj", "Core/Core.Logging/"]
COPY ["Core/Core.Communication/Core.Communication.csproj", "Core/Core.Communication/"]
COPY ["Core/Core.Authentication/Core.Authentication.csproj", "Core/Core.Authentication/"]
COPY ["Services/Venues/Integrations/OpenBrewery/ConventionsAide.Venues.Integration.OpenBrewery.Client/ConventionsAide.Venues.Integration.OpenBrewery.Client.csproj", "Services/Venues/Integrations/OpenBrewery/ConventionsAide.Venues.Integration.OpenBrewery.Client/"]
COPY ["Services/Venues/Integrations/OpenBrewery/ConventionsAide.Venues.Integration.OpenBrewery.Contracts/ConventionsAide.Venues.Integration.OpenBrewery.Contracts.csproj", "Services/Venues/Integrations/OpenBrewery/ConventionsAide.Venues.Integration.OpenBrewery.Contracts/"]
COPY ["Core/Core.HealthChecks/Core.HealthChecks.csproj", "Core/Core.HealthChecks/"]
RUN dotnet restore "Gateways/ConventionsAideGW/ConventionsAideGW.csproj"
COPY . .
WORKDIR "/src/Gateways/ConventionsAideGW"
RUN dotnet build "ConventionsAideGW.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConventionsAideGW.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConventionsAideGW.dll"]