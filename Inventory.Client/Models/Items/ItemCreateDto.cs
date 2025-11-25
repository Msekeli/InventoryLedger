namespace Inventory.Client.Models.Items;

public class ItemCreateDto
{
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int LowStockThreshold { get; set; }
}
