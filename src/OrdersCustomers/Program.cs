using OrdersCustomers.Infra.Data.Extensions;
using OrdersCustomers.Infra.Data.IoC;
using OrdersCustomers.Infra.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfiguration(builder.Configuration);

var app = builder.Build();

app.AddUseConfiguration(app.Environment);

app.UseMiddleware<ExceptionMiddleware>();

app.ApplyMigrations();

app.MapControllers();

app.Run();