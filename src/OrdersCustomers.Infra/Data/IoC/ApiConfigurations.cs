using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OrdersCustomers.Infra.Data.IoC;

public static class ApiConfigurations
{
    public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        InjectorBootstrap.RegisterServices(services, configuration);

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = configuration["Auth0:Authority"];
            options.Audience = configuration["Auth0:Audience"];
        });

        services.AddAuthorization();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Orders Customers - Service",
                Version = "v1",
                Description = "Api Rest criada Pedidos de Clientes.",
                Contact = new OpenApiContact
                {
                    Name = "Gabriel Lanza",
                }

            });

            c.DocInclusionPredicate((docName, apiDesc) => true);
        });

    }

    public static void AddUseConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseSwagger();

        //app.UseSwaggerUI(c =>
        //{
        //    c.RoutePrefix = string.Empty;
        //    c.SwaggerEndpoint("swagger/v1/swagger.json", "OrdersCustomers.API");
        //    c.DocumentTitle = "OrdersCustomers";
        //});

        app.UseHttpsRedirection();

        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}