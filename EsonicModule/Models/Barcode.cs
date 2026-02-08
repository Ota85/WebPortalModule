using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsonicModule.Models;

[Table("Barcodes")]
public class Barcode
{
    [Key]
    public int Id { get; set; }

    public string? Code { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Description { get; set; }
}
