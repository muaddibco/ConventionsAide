#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HealthCheck/MonitorClient/MonitorClient.csproj", "HealthCheck/MonitorClient/"]
COPY ["Core/Core.InitializeServices/Core.InitializeServices.csproj", "Core/Core.InitializeServices/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Logging/Core.Logging.csproj", "Core/Core.Logging/"]
COPY ["Common.Models/Common.Models.csproj", "Common.Models/"]
COPY ["Core/Core.HealthChecks/Core.HealthChecks.csproj", "Core/Core.HealthChecks/"]
COPY ["Core/Core.Communication/Core.Communication.csproj", "Core/Core.Communication/"]
COPY ["Core/ConsumerAPI.Core.Authentication/Core.Authentication.csproj", "Core/ConsumerAPI.Core.Authentication/"]
COPY ["DataAccess/DbAccessors/DbAccessors.Members/DbAccessors.Members.csproj", "DataAccess/DbAccessors/DbAccessors.Members/"]
COPY ["Core/Core.Dal/Core.Dal.csproj", "Core/Core.Dal/"]
COPY ["DataAccess/DbAccessors/Common.DBModels/Common.DBModels.csproj", "DataAccess/DbAccessors/Common.DBModels/"]
COPY ["Core/Core.Services/Core.Services.csproj", "Core/Core.Services/"]
COPY ["Core/Core.Encryption/Core.Cryptography.csproj", "Core/Core.Encryption/"]
COPY ["Core/Core.AzureKeyVault/Core.AzureKeyVault.csproj", "Core/Core.AzureKeyVault/"]
RUN dotnet restore "HealthCheck/MonitorClient/MonitorClient.csproj"
COPY . .
WORKDIR "/src/HealthCheck/MonitorClient"
RUN dotnet build "MonitorClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonitorClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN sed -i 's/SECLEVEL=2/SECLEVEL=1/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "ConsumerAPI.HealthCheck.MonitorClient.dll"]