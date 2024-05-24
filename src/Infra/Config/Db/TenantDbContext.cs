using Microsoft.EntityFrameworkCore;
using multitenancy.Application.Models;
using multitenancy.Application.Repositories;

namespace multitenancy.Infra.Config.Db;

public class TenantDbContext : DbContext
{
    public DbSet<Tenant> Tenants { get; set; }
    protected readonly ConfigurationBuilder ConfigurationBuilder = new();
    protected readonly string ConnectionString;

    public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
    {
        var section = ConfigurationBuilder
            .AddJsonFile("appsettings.json")
            .Build()
            .GetSection("Settings");
        
        ConnectionString = $"Server={section["Server"]},{section["Port"]};" +
                           $"Database={section["Database"]};" +
                           $"User ID={section["User"]};" +
                           $"Password={section["Password"]};" +
                           $"TrustServerCertificate=true;" +
                           $"Trusted_Connection=false;";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(ConnectionString,options=>options.EnableRetryOnFailure())
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>().ToTable("tenant");
    }
}