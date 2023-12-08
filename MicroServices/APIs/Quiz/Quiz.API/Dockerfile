#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MicroServices/APIs/Quiz/Quiz.API/Quiz.API.csproj", "MicroServices/APIs/Quiz/Quiz.API/"]
COPY ["MicroServices/APIs/Quiz/Quiz.DataAccess/Quiz.DataAccess.csproj", "MicroServices/APIs/Quiz/Quiz.DataAccess/"]
COPY ["Domain/Domain.Common/Domain.Common.csproj", "Domain/Domain.Common/"]
RUN dotnet restore "./MicroServices/APIs/Quiz/Quiz.API/./Quiz.API.csproj"
COPY . .
WORKDIR "/src/MicroServices/APIs/Quiz/Quiz.API"
RUN dotnet build "./Quiz.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Quiz.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Quiz.API.dll"]