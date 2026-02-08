using Microsoft.EntityFrameworkCore;
using EsonicModule.Models;

namespace EsonicModule.Data;

public class ZebraDbContext : DbContext
{
    public ZebraDbContext(DbContextOptions<ZebraDbContext> options)
        : base(options)
    {
    }

    public DbSet<Barcode> Barcodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the Barcode entity
        modelBuilder.Entity<Barcode>(entity =>
        {
            entity.ToTable("Barcodes");
            entity.HasKey(e => e.ID);
        });
    }
}
