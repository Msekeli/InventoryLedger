namespace Inventory.Application.Exceptions;

public class InactiveItemException : Exception
{
    public InactiveItemException()
        : base("This item is inactive and cannot be used for this operation.")
    {
    }
}