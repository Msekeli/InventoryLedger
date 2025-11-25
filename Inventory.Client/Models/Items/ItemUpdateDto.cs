namespace Inventory.Client.Models.Items;

public class ItemUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int LowStockThreshold { get; set; }
}
