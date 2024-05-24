using Microsoft.EntityFrameworkCore;
using multitenancy.Application.Models;
using multitenancy.Application.Repositories;

namespace multitenancy.Infra.Config.Db;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public Guid CurrentTenantId { get; set; }
    protected readonly ConfigurationBuilder ConfigurationBuilder = new();
    protected readonly string ConnectionString;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenant currentTenantService) : base(options)
    {
        CurrentTenantId = currentTenantService.TenantId??throw new ArgumentException("Tenant id not found");
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
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("product");
            entity.Property(prod => prod.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(prod => prod.TenantId).HasColumnName("tenant_id");
            entity.HasQueryFilter(a => a.TenantId == CurrentTenantId);
        });
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<HasTenant>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                case EntityState.Modified:
                    entry.Entity.TenantId = CurrentTenantId;
                    break;
            }
        }
        return base.SaveChanges();
    }
}