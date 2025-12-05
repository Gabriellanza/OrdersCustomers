using Microsoft.EntityFrameworkCore;
using OrdersCustomers.Domain.Entities;
using OrdersCustomers.Domain.ValueObjects;

namespace OrdersCustomers.Infra.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

}
