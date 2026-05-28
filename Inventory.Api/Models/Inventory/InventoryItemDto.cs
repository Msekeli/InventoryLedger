namespace Inventory.Api.Models.Inventory;

public class InventoryItemDto
{
    public int Id { get; set; }

    public int ItemId
    {
        get => Id;
        set => Id = value;
    }

    public string SKU { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public int OnHand { get; set; }

    public int CurrentStock
    {
        get => OnHand;
        set => OnHand = value;
    }

    public int LowStockThreshold { get; set; }

    public decimal UnitCostPrice { get; set; }

    public decimal CostPrice
    {
        get => UnitCostPrice;
        set => UnitCostPrice = value;
    }

    public decimal InventoryCostValue { get; set; }

    public bool IsLowStock => OnHand < LowStockThreshold;

    public bool IsLow => IsLowStock;
}
