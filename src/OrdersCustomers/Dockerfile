# Imagem base da aplicação em runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagem do SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/OrdersCustomers/OrdersCustomers.csproj", "OrdersCustomers/"]
COPY ["src/OrdersCustomers.Application/OrdersCustomers.Application.csproj", "OrdersCustomers.Application/"]
COPY ["src/OrdersCustomers.Domain/OrdersCustomers.Domain.csproj", "OrdersCustomers.Domain/"]
COPY ["src/OrdersCustomers.Infra/OrdersCustomers.Infra.csproj", "OrdersCustomers.Infra/"]

COPY src/ ./ 

RUN dotnet restore "OrdersCustomers/OrdersCustomers.csproj"

WORKDIR "/src/OrdersCustomers"
RUN dotnet build "OrdersCustomers.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersCustomers.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OrdersCustomers.dll"]