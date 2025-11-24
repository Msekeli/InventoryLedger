namespace Inventory.Api.Models.Items;

public class ItemResponseDto
{
    public int Id { get; init; }
    public string SKU { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; }
    public int LowStockThreshold { get; init; }
    public int OnHand { get; init; }
    public decimal Value => UnitPrice * OnHand;
}
