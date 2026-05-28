namespace Inventory.Client.Models.Inventory;

public class InventorySummaryDto
{
    public decimal TotalInventoryValue { get; set; }

    public List<InventoryItemDto> Items { get; set; } = new();
}

public class InventoryItemDto
{
    public int Id { get; set; }

    public string SKU { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int OnHand { get; set; }

    public int LowStockThreshold { get; set; }

    public decimal UnitCostPrice { get; set; }

    public decimal InventoryCostValue { get; set; }

    public bool IsLowStock =>
        OnHand < LowStockThreshold;
}