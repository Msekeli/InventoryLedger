namespace Inventory.Client.Models.Items;

public class ItemResponseDto
{
    public int Id { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int LowStockThreshold { get; set; }
    public int OnHand { get; set; }
}
