FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY * .
WORKDIR "/src/Announcements/Announcements.Api"
RUN dotnet build "Announcements.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Announcements.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Announcements.Api.dll"]
