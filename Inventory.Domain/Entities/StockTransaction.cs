using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities;

public class StockTransaction
{
    public int Id { get; set; }

    public int ItemId { get; set; }
    public Item? Item { get; set; }

    public TransactionType TransactionType { get; set; }

    public int Quantity { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public string ReferenceNumber { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;

    public int PerformedByUserId { get; set; }
    public AppUser? PerformedBy { get; set; }
}