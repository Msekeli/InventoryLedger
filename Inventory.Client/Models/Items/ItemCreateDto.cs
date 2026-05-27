namespace Inventory.Client.Models.Items;

public class ItemCreateDto
{
    public string SKU { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public decimal CostPrice { get; set; }

    public decimal SellingPrice { get; set; }

    public int LowStockThreshold { get; set; }

    public int? SupplierId { get; set; }
}