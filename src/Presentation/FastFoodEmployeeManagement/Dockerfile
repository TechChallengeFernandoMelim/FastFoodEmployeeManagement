#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/FastFoodEmployeeManagement/FastFoodEmployeeManagement.csproj", "src/Presentation/FastFoodEmployeeManagement/"]
COPY ["src/Infrastructure/FastFoodEmployeeManagement.Infrastructure.IoC/FastFoodEmployeeManagement.Infrastructure.IoC.csproj", "src/Infrastructure/FastFoodEmployeeManagement.Infrastructure.IoC/"]
COPY ["src/Core/FastFoodEmployeeManagement.Application/FastFoodEmployeeManagement.Application.csproj", "src/Core/FastFoodEmployeeManagement.Application/"]
COPY ["src/Core/FastFoodEmployeeManagement.Domain/FastFoodEmployeeManagement.Domain.csproj", "src/Core/FastFoodEmployeeManagement.Domain/"]
COPY ["src/Infrastructure/FastFoodEmployeeManagement.Infrastructure.Cognito/FastFoodEmployeeManagement.Infrastructure.Cognito.csproj", "src/Infrastructure/FastFoodEmployeeManagement.Infrastructure.Cognito/"]
COPY ["src/Infrastructure/FastFoodEmployeeManagement.Infrastructure.Persistance/FastFoodEmployeeManagement.Infrastructure.Persistance.csproj", "src/Infrastructure/FastFoodEmployeeManagement.Infrastructure.Persistance/"]
RUN dotnet restore "./src/Presentation/FastFoodEmployeeManagement/FastFoodEmployeeManagement.csproj"
COPY . .
WORKDIR "/src/src/Presentation/FastFoodEmployeeManagement"
RUN dotnet build "./FastFoodEmployeeManagement.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FastFoodEmployeeManagement.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastFoodEmployeeManagement.dll"]