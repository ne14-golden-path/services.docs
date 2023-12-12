#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ne14.services.docs.business/ne14.services.docs.business.csproj", "ne14.services.docs.business/"]
COPY ["ne14.services.docs.app/ne14.services.docs.app.csproj", "ne14.services.docs.app/"]
RUN --mount=type=secret,id=nuget_config_file dotnet restore "./ne14.services.docs.app/./ne14.services.docs.app.csproj" --configfile "/run/secrets/nuget_config_file"
COPY . .
WORKDIR "/src/ne14.services.docs.app"
RUN dotnet build "./ne14.services.docs.app.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ne14.services.docs.app.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ne14.services.docs.app.dll"]