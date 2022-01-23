using Microsoft.EntityFrameworkCore;
using SolidMatrix.Affair.Api.Core;
using System.ComponentModel.DataAnnotations;

namespace SolidMatrix.Affair.Api.WarehouseModule;

[Index(nameof(Name), IsUnique = true)]
public class Category : BasicModel
{
    [Required]
    [StringLength(60, MinimumLength = 1)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(10, MinimumLength = 1)]
    public string Initial { get; set; } = null!;
}
