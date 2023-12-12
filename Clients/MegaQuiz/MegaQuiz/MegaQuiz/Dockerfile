#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Clients/MegaQuiz/MegaQuiz/MegaQuiz/MegaQuiz.csproj", "Clients/MegaQuiz/MegaQuiz/MegaQuiz/"]
COPY ["Clients/MegaQuiz/MegaQuiz/MegaQuiz.Client/MegaQuiz.Client.csproj", "Clients/MegaQuiz/MegaQuiz/MegaQuiz.Client/"]
COPY ["Domain/Domain.Common/Domain.Common.csproj", "Domain/Domain.Common/"]
RUN dotnet restore "./Clients/MegaQuiz/MegaQuiz/MegaQuiz/./MegaQuiz.csproj"
COPY . .
WORKDIR "/src/Clients/MegaQuiz/MegaQuiz/MegaQuiz"
RUN dotnet build "./MegaQuiz.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MegaQuiz.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MegaQuiz.dll"]