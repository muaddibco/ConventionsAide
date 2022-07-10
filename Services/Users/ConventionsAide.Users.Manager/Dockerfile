#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Users/ConventionsAide.Users.Manager/ConventionsAide.Users.Manager.csproj", "Services/Users/ConventionsAide.Users.Manager/"]
COPY ["Services/Users/ConventionsAide.Users.EntityFrameworkCore/ConventionsAide.Users.EntityFrameworkCore.csproj", "Services/Users/ConventionsAide.Users.EntityFrameworkCore/"]
COPY ["Core/Core.EntityFrameworkCore/Core.EntityFrameworkCore.csproj", "Core/Core.EntityFrameworkCore/"]
COPY ["Core/Core.Domain/Core.Domain.csproj", "Core/Core.Domain/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
COPY ["Core/Core.Logging/Core.Logging.csproj", "Core/Core.Logging/"]
COPY ["Services/Users/ConventionsAide.Users.Domain/ConventionsAide.Users.Domain.csproj", "Services/Users/ConventionsAide.Users.Domain/"]
COPY ["Core/Core.Communication/Core.Communication.csproj", "Core/Core.Communication/"]
COPY ["Core/Core.Authentication/Core.Authentication.csproj", "Core/Core.Authentication/"]
COPY ["Services/Users/ConventionsAide.Users.Contracts/ConventionsAide.Users.Contracts.csproj", "Services/Users/ConventionsAide.Users.Contracts/"]
COPY ["Core/ConventionsAide.Core.ObjectMapping/Core.ObjectMapping.csproj", "Core/ConventionsAide.Core.ObjectMapping/"]
COPY ["Core/Core.Services/Core.Services.csproj", "Core/Core.Services/"]
RUN dotnet restore "Services/Users/ConventionsAide.Users.Manager/ConventionsAide.Users.Manager.csproj"
COPY . .
WORKDIR "/src/Services/Users/ConventionsAide.Users.Manager"
RUN dotnet build "ConventionsAide.Users.Manager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConventionsAide.Users.Manager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConventionsAide.Users.Manager.dll"]