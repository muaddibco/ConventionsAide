#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/TalkOrders/ConventionsAide.TalkOrders.Manager/ConventionsAide.TalkOrders.Manager.csproj", "Services/TalkOrders/ConventionsAide.TalkOrders.Manager/"]
COPY ["Core/Core.HealthChecks/Core.HealthChecks.csproj", "Core/Core.HealthChecks/"]
COPY ["Core/Core.Communication/Core.Communication.csproj", "Core/Core.Communication/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Logging/Core.Logging.csproj", "Core/Core.Logging/"]
COPY ["Core/Core.Authentication/Core.Authentication.csproj", "Core/Core.Authentication/"]
COPY ["Services/TalkOrders/ConventionsAide.TalkOrders.Contracts/ConventionsAide.TalkOrders.Contracts.csproj", "Services/TalkOrders/ConventionsAide.TalkOrders.Contracts/"]
COPY ["Core/Core.Contracts/Core.Contracts.csproj", "Core/Core.Contracts/"]
COPY ["Services/TalkOrders/ConventionsAide.TalkOrders.Domain.Shared/ConventionsAide.TalkOrders.Domain.Shared.csproj", "Services/TalkOrders/ConventionsAide.TalkOrders.Domain.Shared/"]
COPY ["Core/ConventionsAide.Core.ObjectMapping/Core.ObjectMapping.csproj", "Core/ConventionsAide.Core.ObjectMapping/"]
COPY ["Services/TalkOrders/ConventionsAide.TalkOrders.EntityFrameworkCore/ConventionsAide.TalkOrders.EntityFrameworkCore.csproj", "Services/TalkOrders/ConventionsAide.TalkOrders.EntityFrameworkCore/"]
COPY ["Core/Core.Services/Core.Services.csproj", "Core/Core.Services/"]
COPY ["Services/TalkOrders/ConventionsAide.TalkOrders.Domain/ConventionsAide.TalkOrders.Domain.csproj", "Services/TalkOrders/ConventionsAide.TalkOrders.Domain/"]
COPY ["Core/Core.Domain/Core.Domain.csproj", "Core/Core.Domain/"]
RUN dotnet restore "Services/TalkOrders/ConventionsAide.TalkOrders.Manager/ConventionsAide.TalkOrders.Manager.csproj"
COPY . .
WORKDIR "/src/Services/TalkOrders/ConventionsAide.TalkOrders.Manager"
RUN dotnet build "ConventionsAide.TalkOrders.Manager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConventionsAide.TalkOrders.Manager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConventionsAide.TalkOrders.Manager.dll"]