FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/OrdersCustomers.Worker/OrdersCustomers.Worker.csproj", "OrdersCustomers.Worker/"]

COPY src/ ./

RUN dotnet restore "OrdersCustomers.Worker/OrdersCustomers.Worker.csproj"

WORKDIR "/src/OrdersCustomers.Worker"
RUN dotnet build "OrdersCustomers.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersCustomers.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OrdersCustomers.Worker.dll"]