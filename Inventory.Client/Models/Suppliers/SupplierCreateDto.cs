namespace Inventory.Client.Models.Suppliers;

public class SupplierCreateDto
{
    public string SupplierCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string ContactPerson { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;
}