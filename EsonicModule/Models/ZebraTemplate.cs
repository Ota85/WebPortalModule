using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsonicModule.Models;

[Table("zebra_template")]
public class ZebraTemplate
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(255)]
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(max)")]
    public string Template { get; set; } = string.Empty;
}