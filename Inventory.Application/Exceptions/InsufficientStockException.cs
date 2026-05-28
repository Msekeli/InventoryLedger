namespace Inventory.Application.Exceptions;

public class InsufficientStockException : Exception
{
    public InsufficientStockException()
        : base("Insufficient stock available for this operation.")
    {
    }
}