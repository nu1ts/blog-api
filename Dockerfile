FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

RUN apt-get update && apt-get install -y postgresql-client && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY src/ .
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .

COPY restore/ /restore/

RUN chmod +x /restore/restore-db.sh

LABEL MAINTAINER="Лугачёв Никита Владимирович <nikitalugachev149th@gmail.com>"
ENV APP_VERSION="1.0.0"

RUN groupadd -r appgroup && useradd -r -g appgroup appuser
USER appuser

ENTRYPOINT ["/bin/bash", "-c", "/restore/restore-db.sh && dotnet src.dll"]