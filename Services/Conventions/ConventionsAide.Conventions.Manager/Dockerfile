#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Conventions/ConventionsAide.Conventions.Manager/ConventionsAide.Conventions.Manager.csproj", "Services/Conventions/ConventionsAide.Conventions.Manager/"]
COPY ["Core/Core.Communication/Core.Communication.csproj", "Core/Core.Communication/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Logging/Core.Logging.csproj", "Core/Core.Logging/"]
COPY ["Core/Core.Authentication/Core.Authentication.csproj", "Core/Core.Authentication/"]
COPY ["Core/Core.HealthChecks/Core.HealthChecks.csproj", "Core/Core.HealthChecks/"]
COPY ["Core/ConventionsAide.Core.ObjectMapping/Core.ObjectMapping.csproj", "Core/ConventionsAide.Core.ObjectMapping/"]
COPY ["Core/Core.Services/Core.Services.csproj", "Core/Core.Services/"]
RUN dotnet restore "Services/Conventions/ConventionsAide.Conventions.Manager/ConventionsAide.Conventions.Manager.csproj"
COPY . .
WORKDIR "/src/Services/Conventions/ConventionsAide.Conventions.Manager"
RUN dotnet build "ConventionsAide.Conventions.Manager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConventionsAide.Conventions.Manager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConventionsAide.Conventions.Manager.dll"]