#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/VenueOrders/ConventionsAide.VenueOrders.Manager/ConventionsAide.VenueOrders.Manager.csproj", "Services/VenueOrders/ConventionsAide.VenueOrders.Manager/"]
COPY ["Core/Core.Communication/Core.Communication.csproj", "Core/Core.Communication/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Logging/Core.Logging.csproj", "Core/Core.Logging/"]
COPY ["Core/Core.Authentication/Core.Authentication.csproj", "Core/Core.Authentication/"]
COPY ["Services/VenueOrders/ConventionsAide.VenueOrders.Domain/ConventionsAide.VenueOrders.Domain.csproj", "Services/VenueOrders/ConventionsAide.VenueOrders.Domain/"]
COPY ["Services/VenueOrders/ConventionsAide.VenueOrders.Domain.Shared/ConventionsAide.VenueOrders.Domain.Shared.csproj", "Services/VenueOrders/ConventionsAide.VenueOrders.Domain.Shared/"]
COPY ["Core/Core.Domain/Core.Domain.csproj", "Core/Core.Domain/"]
COPY ["Services/VenueOrders/ConventionsAide.VenueOrders.Contracts/ConventionsAide.VenueOrders.Contracts.csproj", "Services/VenueOrders/ConventionsAide.VenueOrders.Contracts/"]
COPY ["Core/Core.Contracts/Core.Contracts.csproj", "Core/Core.Contracts/"]
COPY ["Core/ConventionsAide.Core.ObjectMapping/Core.ObjectMapping.csproj", "Core/ConventionsAide.Core.ObjectMapping/"]
COPY ["Services/VenueOrders/ConventionsAide.VenueOrders.EntityFrameworkCore/ConventionsAide.VenueOrders.EntityFrameworkCore.csproj", "Services/VenueOrders/ConventionsAide.VenueOrders.EntityFrameworkCore/"]
COPY ["Core/Core.EntityFrameworkCore/Core.EntityFrameworkCore.csproj", "Core/Core.EntityFrameworkCore/"]
COPY ["Core/Core.Services/Core.Services.csproj", "Core/Core.Services/"]
RUN dotnet restore "Services/VenueOrders/ConventionsAide.VenueOrders.Manager/ConventionsAide.VenueOrders.Manager.csproj"
COPY . .
WORKDIR "/src/Services/VenueOrders/ConventionsAide.VenueOrders.Manager"
RUN dotnet build "ConventionsAide.VenueOrders.Manager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConventionsAide.VenueOrders.Manager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConventionsAide.VenueOrders.Manager.dll"]