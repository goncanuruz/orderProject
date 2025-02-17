# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/OrderProject.WebAPI/OrderProject.WebAPI.csproj", "Presentation/OrderProject.WebAPI/"]
COPY ["Core/OrderProject.Application/OrderProject.Application.csproj", "Core/OrderProject.Application/"]
COPY ["Core/OrderProject.Domain/OrderProject.Domain.csproj", "Core/OrderProject.Domain/"]
COPY ["Infrastructure/OrderProject.Infrastructure/OrderProject.Infrastructure.csproj", "Infrastructure/OrderProject.Infrastructure/"]
COPY ["Infrastructure/OrderProject.Persistence/OrderProject.Persistence.csproj", "Infrastructure/OrderProject.Persistence/"]
RUN dotnet restore "./Presentation/OrderProject.WebAPI/OrderProject.WebAPI.csproj"
COPY . .
WORKDIR "/src/Presentation/OrderProject.WebAPI"
RUN dotnet build "./OrderProject.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderProject.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderProject.WebAPI.dll"]