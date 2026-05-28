namespace Inventory.Domain.Enums;

public enum TransactionType
{
    StockReceived = 1,
    Sale = 2,
    CustomerReturn = 3,
    SupplierReturn = 4,
    Damaged = 5,
    Expired = 6,
    Adjustment = 7,
    StockCountCorrection = 8
}