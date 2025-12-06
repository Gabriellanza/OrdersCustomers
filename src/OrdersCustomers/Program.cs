using OrdersCustomers.Infra.Data.Extensions;
using OrdersCustomers.Infra.Data.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();

app.AddUseConfiguration(app.Environment);

app.ApplyMigrations();

app.MapControllers();

app.Run();