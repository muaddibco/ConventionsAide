#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Conventions/ConventionsAide.Conventions.Manager/ConventionsAide.Conventions.Manager.csproj", "Conventions/ConventionsAide.Conventions.Manager/"]
RUN dotnet restore "Conventions/ConventionsAide.Conventions.Manager/ConventionsAide.Conventions.Manager.csproj"
COPY . .
WORKDIR "/src/Conventions/ConventionsAide.Conventions.Manager"
RUN dotnet build "ConventionsAide.Conventions.Manager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConventionsAide.Conventions.Manager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConventionsAide.Conventions.Manager.dll"]