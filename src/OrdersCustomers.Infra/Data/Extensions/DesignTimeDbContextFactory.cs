using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrdersCustomers.Infra.Data.Context;

namespace OrdersCustomers.Infra.Data.Extensions;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Host=postgres;Port=5432;Database=OrdersCustomers;Username=admin;Password=admin");

        return new AppDbContext(optionsBuilder.Options);
    }
}