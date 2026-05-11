namespace Inventory.Domain.Entities;

public class Sale
{
    public int Id { get; set; }

    public string SaleNumber { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public decimal TotalAmount { get; set; }

    public int ProcessedByUserId { get; set; }
    public AppUser? ProcessedBy { get; set; }

    public ICollection<SaleLine> SaleLines { get; set; } = new List<SaleLine>();
}