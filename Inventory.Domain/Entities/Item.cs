namespace Inventory.Domain.Entities;

public class Item
{
    public int Id { get; set; }

    public string SKU { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public decimal CostPrice { get; set; }
    public decimal SellingPrice { get; set; }

    public int LowStockThreshold { get; set; }

    public bool IsActive { get; set; } = true;

    public int SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    public bool IsLowStock(int currentQuantity)
    {
        return currentQuantity < LowStockThreshold;
    }
}