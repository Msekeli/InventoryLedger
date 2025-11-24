namespace Inventory.Domain.Entities;

public class Item
{
    public int Id { get; set; }                 // EF will use this as PK
    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int LowStockThreshold { get; set; }

    // Helper: Check if item is below threshold (used in API or Application layer)
    public bool IsLowStock(int currentQuantity)
    {
        return currentQuantity < LowStockThreshold;
    }
}
