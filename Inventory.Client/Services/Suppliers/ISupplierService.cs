using Inventory.Client.Models.Suppliers;

namespace Inventory.Client.Services.Suppliers;

public interface ISupplierService
{
    Task<List<SupplierDto>> GetAllAsync();

    Task<bool> CreateAsync(SupplierCreateDto dto);
}