using HealthCheck.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCheck.Api.Context;

public class HealthContext : DbContext
{
    public HealthContext(DbContextOptions<HealthContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
}
