using Microsoft.EntityFrameworkCore;

namespace EsonicModule.Data;

public class SAPDataDbContext : DbContext
{
    public SAPDataDbContext(DbContextOptions<SAPDataDbContext> options)
        : base(options)
    {
    }

    // Add DbSet properties for your SAP Data entities here
    // Example: public DbSet<YourEntity> YourEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure your SAP Data entities here
        // Example:
        // modelBuilder.Entity<YourEntity>(entity =>
        // {
        //     entity.ToTable("YourTableName");
        //     entity.HasKey(e => e.Id);
        // });
    }
}
