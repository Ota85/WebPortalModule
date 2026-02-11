namespace EsonicModule.DTOs;

public class MaterialStockStageDto
{
    public string? Plant { get; set; }
    public int? MaterialNumber { get; set; }
    public string? MaterialDescription { get; set; }
    public string? MaterialBatch { get; set; }
    public string? Storage { get; set; }
    public string? ExternalBatch { get; set; }
    public string? UnitOfMeasurement { get; set; }
    public double? Quantity { get; set; }
    public DateTime? TimeStamp { get; set; }
    public double? AlphaAcid { get; set; }
    public double? Extract { get; set; }
    public double? ZExtract { get; set; }
    public byte? Processed { get; set; }
    public DateTime? ProcessedTimeStamp { get; set; }
}
