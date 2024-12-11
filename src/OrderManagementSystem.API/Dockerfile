FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/OrderManagementSystem.API/OrderManagementSystem.API.csproj", "src/OrderManagementSystem.API/"]
COPY ["src/OrderManagementSystem.Application/OrderManagementSystem.Application.csproj", "src/OrderManagementSystem.Application/"]
COPY ["src/OrderManagementSystem.Domain/OrderManagementSystem.Domain.csproj", "src/OrderManagementSystem.Domain/"]
COPY ["src/OrderManagementSystem.Infrastructure/OrderManagementSystem.Infrastructure.csproj", "src/OrderManagementSystem.Infrastructure/"]
RUN dotnet restore "./src/OrderManagementSystem.API/./OrderManagementSystem.API.csproj"
COPY . .
WORKDIR "/src/src/OrderManagementSystem.API"
RUN dotnet build "./OrderManagementSystem.API.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderManagementSystem.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManagementSystem.API.dll"]
