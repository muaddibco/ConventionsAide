#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Venues/ConventionsAide.Venues.Manager/ConventionsAide.Venues.Manager.csproj", "Services/Venues/ConventionsAide.Venues.Manager/"]
COPY ["Core/Core.Communication/Core.Communication.csproj", "Core/Core.Communication/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Logging/Core.Logging.csproj", "Core/Core.Logging/"]
COPY ["Core/Core.Authentication/Core.Authentication.csproj", "Core/Core.Authentication/"]
COPY ["Services/Venues/ConventionsAide.Venues.Contracts/ConventionsAide.Venues.Contracts.csproj", "Services/Venues/ConventionsAide.Venues.Contracts/"]
COPY ["Core/Core.Contracts/Core.Contracts.csproj", "Core/Core.Contracts/"]
COPY ["Services/Venues/ConventionsAide.Venues.Domain/ConventionsAide.Venues.Domain.csproj", "Services/Venues/ConventionsAide.Venues.Domain/"]
COPY ["Core/Core.Domain/Core.Domain.csproj", "Core/Core.Domain/"]
COPY ["Core/ConventionsAide.Core.ObjectMapping/Core.ObjectMapping.csproj", "Core/ConventionsAide.Core.ObjectMapping/"]
COPY ["Services/Venues/ConventionsAide.Venues.EntityFrameworkCore/ConventionsAide.Venues.EntityFrameworkCore.csproj", "Services/Venues/ConventionsAide.Venues.EntityFrameworkCore/"]
COPY ["Core/Core.EntityFrameworkCore/Core.EntityFrameworkCore.csproj", "Core/Core.EntityFrameworkCore/"]
COPY ["Core/Core.Services/Core.Services.csproj", "Core/Core.Services/"]
RUN dotnet restore "Services/Venues/ConventionsAide.Venues.Manager/ConventionsAide.Venues.Manager.csproj"
COPY . .
WORKDIR "/src/Services/Venues/ConventionsAide.Venues.Manager"
RUN dotnet build "ConventionsAide.Venues.Manager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConventionsAide.Venues.Manager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConventionsAide.Venues.Manager.dll"]