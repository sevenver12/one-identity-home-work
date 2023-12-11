#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/OneIdentity.Homework.Api/OneIdentity.Homework.Api.csproj", "src/OneIdentity.Homework.Api/"]
COPY ["src/OneIdentity.Homework.Api.ServiceDefaults/OneIdentity.Homework.ServiceDefaults.csproj", "src/OneIdentity.Homework.Api.ServiceDefaults/"]
COPY ["src/OneIdentity.Homework.Database/OneIdentity.Homework.Database.csproj", "src/OneIdentity.Homework.Database/"]
COPY ["src/OneIdentity.Homework.Repository/OneIdentity.Homework.Repository.csproj", "src/OneIdentity.Homework.Repository/"]
RUN dotnet restore "./src/OneIdentity.Homework.Api/./OneIdentity.Homework.Api.csproj"
COPY . .
WORKDIR "/src/src/OneIdentity.Homework.Api"
RUN dotnet build "./OneIdentity.Homework.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OneIdentity.Homework.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OneIdentity.Homework.Api.dll"]