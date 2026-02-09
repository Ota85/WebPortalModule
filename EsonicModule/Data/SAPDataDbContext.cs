using Microsoft.EntityFrameworkCore;
using EsonicModule.Models;

namespace EsonicModule.Data;

public class SAPDataDbContext : DbContext
{
    public SAPDataDbContext(DbContextOptions<SAPDataDbContext> options)
        : base(options)
    {
    }

    public DbSet<PrinterSetting> PrinterSettings { get; set; }
    public DbSet<ZebraTemplate> ZebraTemplates { get; set; }
    public DbSet<MaterialStockStage> MaterialStockStages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure PrinterSetting entity
        modelBuilder.Entity<PrinterSetting>(entity =>
        {
            entity.ToTable("printer_settings");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IPAddress).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Port).IsRequired();
        });

        // Configure ZebraTemplate entity
        modelBuilder.Entity<ZebraTemplate>(entity =>
        {
            entity.ToTable("zebra_template");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Template).IsRequired().HasColumnType("nvarchar(max)");
        });

        // Configure MaterialStockStage entity
        modelBuilder.Entity<MaterialStockStage>(entity =>
        {
            entity.ToTable("material_stock_stage");
            // No primary key defined as per the table structure
            entity.HasNoKey();
            entity.Property(e => e.Plant).HasMaxLength(10);
            entity.Property(e => e.MaterialDescription).HasMaxLength(100);
            entity.Property(e => e.MaterialBatch).HasMaxLength(50);
            entity.Property(e => e.Storage).HasMaxLength(5);
            entity.Property(e => e.ExternalBatch).HasMaxLength(100);
            entity.Property(e => e.UnitOfMeasurement).HasMaxLength(5);
            entity.Property(e => e.Quantity).HasColumnType("float");
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
            entity.Property(e => e.AlphaAcid).HasColumnType("float");
            entity.Property(e => e.Extract).HasColumnType("float");
            entity.Property(e => e.ZExtract).HasColumnType("float");
            entity.Property(e => e.Processed).HasColumnType("tinyint");
            entity.Property(e => e.ProcessedTimeStamp).HasColumnType("datetime");
        });
    }
}
