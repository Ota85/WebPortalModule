using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsonicModule.Models;

[Table("material_stock_stage")]
public class MaterialStockStage
{
    [MaxLength(10)]
    public string? Plant { get; set; }

    public int? MaterialNumber { get; set; }

    [MaxLength(100)]
    public string? MaterialDescription { get; set; }

    [MaxLength(50)]
    public string? MaterialBatch { get; set; }

    [MaxLength(5)]
    public string? Storage { get; set; }

    [MaxLength(100)]
    public string? ExternalBatch { get; set; }

    [MaxLength(5)]
    public string? UnitOfMeasurement { get; set; }

    public double? Quantity { get; set; }

    public DateTime? TimeStamp { get; set; }

    public double? AlphaAcid { get; set; }

    public double? Extract { get; set; }

    public double? ZExtract { get; set; }

    public byte? Processed { get; set; }

    public DateTime? ProcessedTimeStamp { get; set; }
}