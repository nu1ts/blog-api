FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/ .
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .

COPY restore-db.sh /docker-entrypoint-initdb.d/restore-db.sh
RUN chmod +x /docker-entrypoint-initdb.d/restore-db.sh

ENTRYPOINT ["dotnet", "src.dll"]