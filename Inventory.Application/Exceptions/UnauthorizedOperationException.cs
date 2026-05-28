namespace Inventory.Application.Exceptions;

public class UnauthorizedOperationException : Exception
{
    public UnauthorizedOperationException()
        : base("You are not authorized to perform this operation.")
    {
    }
}