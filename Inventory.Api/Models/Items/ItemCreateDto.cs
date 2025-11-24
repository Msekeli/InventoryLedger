using System.ComponentModel.DataAnnotations;

namespace Inventory.Api.Models.Items;

public class ItemCreateDto
{
    [Required, MinLength(1)]
    public string SKU { get; set; } = string.Empty;

    [Required, MinLength(1)]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [Range(0, int.MaxValue)]
    public int LowStockThreshold { get; set; }
}
