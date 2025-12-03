FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["OrdersCustomers.Application/OrdersCustomers.Application.csproj", "OrdersCustomers.Application/"]
COPY ["OrdersCustomers.Domain/OrdersCustomers.Domain.csproj", "OrdersCustomers.Domain/"]
COPY ["OrdersCustomers.DTO/OrdersCustomers.DTO.csproj", "OrdersCustomers.DTO/"]
COPY ["OrdersCustomers.Infra/OrdersCustomers.Infra.csproj", "OrdersCustomers.Infra/"]
COPY ["OrdersCustomers.Worker/OrdersCustomers.Worker.csproj", "OrdersCustomers.Worker/"]

RUN dotnet restore "OrdersCustomers.Worker/OrdersCustomers.Worker.csproj"
COPY . .

WORKDIR "/src/OrdersCustomers.Worker"
RUN dotnet build "OrdersCustomers.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrdersCustomers.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrdersCustomers.Worker.dll"]