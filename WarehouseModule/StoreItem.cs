using Microsoft.EntityFrameworkCore;
using SolidMatrix.Affair.Api.Core;
using System.ComponentModel.DataAnnotations;

namespace SolidMatrix.Affair.Api.WarehouseModule;

[Index(nameof(Code), IsUnique = true)]
[Index(nameof(Pattern))]
[Index(nameof(Location))]
public class StoreItem : BasicModel
{
    [StringLength(50)]
    public string Pattern { get; set; } = "";

    [StringLength(50)]
    public string Color { get; set; } = "";

    [StringLength(50)]
    public string Location { get; set; } = "";

    [Required]
    [Range(0, 999.99)]
    public decimal Quantity { get; set; }

    [Required]
    [RegularExpression(@"^\d{3}-\d{2}-\d{6}-\d{5}$")]
    public string Code { get; set; } = null!;

    [Required]
    public ItemStatus Status { get; set; } = ItemStatus.Initial;

    [Required]
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

}
