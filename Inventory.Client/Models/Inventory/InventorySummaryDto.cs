namespace Inventory.Client.Models.Inventory;

public class InventorySummaryDto
{
    public List<InventoryItemDto> Items { get; set; } = new();

    public decimal TotalInventoryValue => Items.Sum(i => i.Value);
}

public class InventoryItemDto
{
    public int ItemId { get; set; }
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public int OnHand { get; set; }
    public int LowStockThreshold { get; set; }
    public decimal UnitPrice { get; set; }

    // *** THIS IS THE CORRECT PROPERTY NAME ***
    public bool IsLowStock => OnHand < LowStockThreshold;

    public decimal Value => UnitPrice * OnHand;
}
