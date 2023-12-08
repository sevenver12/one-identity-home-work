#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["OneIdentity.Homework.Api/OneIdentity.Homework.Api.csproj", "OneIdentity.Homework.Api/"]
COPY ["src/OneIdentity.Homework.Api.ServiceDefaults/OneIdentity.Homework.ServiceDefaults.csproj", "src/OneIdentity.Homework.Api.ServiceDefaults/"]
RUN dotnet restore "./OneIdentity.Homework.Api/./OneIdentity.Homework.Api.csproj"
COPY . .
WORKDIR "/src/OneIdentity.Homework.Api"
RUN dotnet build "./OneIdentity.Homework.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OneIdentity.Homework.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["./OneIdentity.Homework.Api"]