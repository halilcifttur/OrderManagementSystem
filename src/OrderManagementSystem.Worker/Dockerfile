FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/OrderManagementSystem.Worker/OrderManagementSystem.Worker.csproj", "src/OrderManagementSystem.Worker/"]
COPY ["src/OrderManagementSystem.Domain/OrderManagementSystem.Domain.csproj", "src/OrderManagementSystem.Domain/"]
RUN dotnet restore "./src/OrderManagementSystem.Worker/./OrderManagementSystem.Worker.csproj"
COPY . .
WORKDIR "/src/src/OrderManagementSystem.Worker"
RUN dotnet build "./OrderManagementSystem.Worker.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OrderManagementSystem.Worker.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManagementSystem.Worker.dll"]