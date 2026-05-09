using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string SupplierCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}