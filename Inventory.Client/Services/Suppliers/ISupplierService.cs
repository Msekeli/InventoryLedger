using Inventory.Client.Models.Suppliers;

namespace Inventory.Client.Services.Suppliers;

public interface ISupplierService
{
    Task<List<SupplierDto>> GetAllAsync();
}