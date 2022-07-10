#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gateways/UsersAideGW/UsersAideGW.csproj", "Gateways/UsersAideGW/"]
COPY ["Core/Core.Services/Core.Services.csproj", "Core/Core.Services/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Logging/Core.Logging.csproj", "Core/Core.Logging/"]
COPY ["Core/Core.Communication/Core.Communication.csproj", "Core/Core.Communication/"]
COPY ["Core/Core.Authentication/Core.Authentication.csproj", "Core/Core.Authentication/"]
COPY ["Core/ConventionsAide.Core.ObjectMapping/Core.ObjectMapping.csproj", "Core/ConventionsAide.Core.ObjectMapping/"]
COPY ["Services/Users/ConventionsAide.Users.Client/ConventionsAide.Users.Client.csproj", "Services/Users/ConventionsAide.Users.Client/"]
COPY ["Services/Users/ConventionsAide.Users.Contracts/ConventionsAide.Users.Contracts.csproj", "Services/Users/ConventionsAide.Users.Contracts/"]
COPY ["Core/Core.HealthChecks/Core.HealthChecks.csproj", "Core/Core.HealthChecks/"]
RUN dotnet restore "Gateways/UsersAideGW/UsersAideGW.csproj"
COPY . .
WORKDIR "/src/Gateways/UsersAideGW"
RUN dotnet build "UsersAideGW.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UsersAideGW.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UsersAideGW.dll"]