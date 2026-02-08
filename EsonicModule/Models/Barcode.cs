using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsonicModule.Models;

[Table("Barcodes")]
public class Barcode
{
    [Key]
    public int ID { get; set; }

    public string? ZPLCode { get; set; }
}
