using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsonicModule.Models;

[Table("printer_settings")]
public class PrinterSetting
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(50)]
    public string IPAddress { get; set; } = string.Empty;

    [Required]
    public int Port { get; set; }
}