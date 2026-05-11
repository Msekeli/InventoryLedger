namespace Inventory.Domain.Entities;

public class SaleLine
{
    public int Id { get; set; }

    public int SaleId { get; set; }
    public Sale? Sale { get; set; }

    public int ItemId { get; set; }
    public Item? Item { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal LineTotal => Quantity * UnitPrice;
}