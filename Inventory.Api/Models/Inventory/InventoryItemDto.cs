namespace Inventory.Api.Models.Inventory;

public class InventoryItemDto
{
    public int ItemId { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public int OnHand { get; set; }
    public int LowStockThreshold { get; set; }

    public bool IsLow => OnHand < LowStockThreshold;
}
