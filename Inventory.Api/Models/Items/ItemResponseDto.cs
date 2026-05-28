namespace Inventory.Api.Models.Items;

public class ItemResponseDto
{
    public int Id { get; init; }

    public string SKU { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public decimal CostPrice { get; init; }

    public decimal SellingPrice { get; init; }

    public int LowStockThreshold { get; init; }

    public int SupplierId { get; init; }

    public int OnHand { get; init; }

    public decimal Value => CostPrice * OnHand;
}