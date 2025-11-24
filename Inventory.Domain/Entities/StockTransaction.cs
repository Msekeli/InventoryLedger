namespace Inventory.Domain.Entities;

public class StockTransaction
{
    public int Id { get; set; }                 // PK
    public int ItemId { get; set; }             // FK in Infrastructure
    public int QuantityChange { get; set; }     // + for in, - for out
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public string Reference { get; set; } = string.Empty;

    // Navigation property (EF will use it in Infrastructure)
    public Item? Item { get; set; }

    // Helper: distinguish positive vs negative change
    public bool IsIncrease => QuantityChange > 0;
    public bool IsDecrease => QuantityChange < 0;
}
