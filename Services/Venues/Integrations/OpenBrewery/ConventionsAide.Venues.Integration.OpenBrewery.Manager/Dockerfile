#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Venues/Integrations/OpenBrewery/ConventionsAide.Venues.Integration.OpenBrewery.Manager/ConventionsAide.Venues.Integration.OpenBrewery.Manager.csproj", "Services/Venues/Integrations/OpenBrewery/ConventionsAide.Venues.Integration.OpenBrewery.Manager/"]
COPY ["Core/Core.Services/Core.Services.csproj", "Core/Core.Services/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Logging/Core.Logging.csproj", "Core/Core.Logging/"]
COPY ["Core/ConventionsAide.Core.ObjectMapping/Core.ObjectMapping.csproj", "Core/ConventionsAide.Core.ObjectMapping/"]
COPY ["Services/Venues/ConventionsAide.Venues.Client/ConventionsAide.Venues.Client.csproj", "Services/Venues/ConventionsAide.Venues.Client/"]
COPY ["Core/Core.Communication/Core.Communication.csproj", "Core/Core.Communication/"]
COPY ["Core/Core.Authentication/Core.Authentication.csproj", "Core/Core.Authentication/"]
COPY ["Services/Venues/ConventionsAide.Venues.Contracts/ConventionsAide.Venues.Contracts.csproj", "Services/Venues/ConventionsAide.Venues.Contracts/"]
COPY ["Core/Core.Contracts/Core.Contracts.csproj", "Core/Core.Contracts/"]
COPY ["Services/Venues/Integrations/OpenBrewery/ConventionsAide.Venues.Integration.OpenBrewery.Contracts/ConventionsAide.Venues.Integration.OpenBrewery.Contracts.csproj", "Services/Venues/Integrations/OpenBrewery/ConventionsAide.Venues.Integration.OpenBrewery.Contracts/"]
RUN dotnet restore "Services/Venues/Integrations/OpenBrewery/ConventionsAide.Venues.Integration.OpenBrewery.Manager/ConventionsAide.Venues.Integration.OpenBrewery.Manager.csproj"
COPY . .
WORKDIR "/src/Services/Venues/Integrations/OpenBrewery/ConventionsAide.Venues.Integration.OpenBrewery.Manager"
RUN dotnet build "ConventionsAide.Venues.Integration.OpenBrewery.Manager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConventionsAide.Venues.Integration.OpenBrewery.Manager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConventionsAide.Venues.Integration.OpenBrewery.Manager.dll"]