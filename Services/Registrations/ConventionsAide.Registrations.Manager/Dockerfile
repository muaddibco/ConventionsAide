#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Registrations/ConventionsAide.Registrations.Manager/ConventionsAide.Registrations.Manager.csproj", "Services/Registrations/ConventionsAide.Registrations.Manager/"]
COPY ["Core/Core.HealthChecks/Core.HealthChecks.csproj", "Core/Core.HealthChecks/"]
COPY ["Core/Core.Communication/Core.Communication.csproj", "Core/Core.Communication/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Logging/Core.Logging.csproj", "Core/Core.Logging/"]
COPY ["Core/Core.Authentication/Core.Authentication.csproj", "Core/Core.Authentication/"]
COPY ["Services/Registrations/ConventionsAide.Registrations.Domain/ConventionsAide.Registrations.Domain.csproj", "Services/Registrations/ConventionsAide.Registrations.Domain/"]
COPY ["Services/Users/ConventionsAide.Users.Domain.Shared/ConventionsAide.Users.Domain.Shared.csproj", "Services/Users/ConventionsAide.Users.Domain.Shared/"]
COPY ["Services/Registrations/ConventionsAide.Registrations.Domain.Shared/ConventionsAide.Registrations.Domain.Shared.csproj", "Services/Registrations/ConventionsAide.Registrations.Domain.Shared/"]
COPY ["Core/Core.Domain/Core.Domain.csproj", "Core/Core.Domain/"]
COPY ["Services/Registrations/ConventionsAide.Registrations.Contracts/ConventionsAide.Registrations.Contracts.csproj", "Services/Registrations/ConventionsAide.Registrations.Contracts/"]
COPY ["Core/Core.Contracts/Core.Contracts.csproj", "Core/Core.Contracts/"]
COPY ["Core/ConventionsAide.Core.ObjectMapping/Core.ObjectMapping.csproj", "Core/ConventionsAide.Core.ObjectMapping/"]
COPY ["Services/Registrations/ConventionsAide.Registrations.EntityFrameworkCore/ConventionsAide.Registrations.EntityFrameworkCore.csproj", "Services/Registrations/ConventionsAide.Registrations.EntityFrameworkCore/"]
COPY ["Core/Core.Services/Core.Services.csproj", "Core/Core.Services/"]
RUN dotnet restore "Services/Registrations/ConventionsAide.Registrations.Manager/ConventionsAide.Registrations.Manager.csproj"
COPY . .
WORKDIR "/src/Services/Registrations/ConventionsAide.Registrations.Manager"
RUN dotnet build "ConventionsAide.Registrations.Manager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConventionsAide.Registrations.Manager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConventionsAide.Registrations.Manager.dll"]