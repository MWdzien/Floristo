ARG DOTNET_VERSION=9.0
ARG BUILD_CONFIGURATION=Release

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
ARG BUILD_CONFIGURATION
WORKDIR /src
COPY src/Floristo.Api/Floristo.Api.csproj Floristo.Api/
COPY src/Floristo.Application/Floristo.Application.csproj Floristo.Application/
COPY src/Floristo.Infrastructure/Floristo.Infrastructure.csproj Floristo.Infrastructure/
COPY src/Floristo.Domain/Floristo.Domain.csproj Floristo.Domain/
COPY src/Floristo.Shared/Floristo.Shared.csproj Floristo.Shared/
COPY src/Floristo.SharedAbstractions/Floristo.SharedAbstractions.csproj Floristo.SharedAbstractions/

RUN dotnet restore Floristo.Api/Floristo.Api.csproj

COPY src/ .

RUN dotnet publish Floristo.Api/Floristo.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS runtime
WORKDIR /app
COPY --from=build /app/publish .
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT [ "dotnet", "Floristo.Api.dll" ]

