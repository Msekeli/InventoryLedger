using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface ISupplierService
{
    Task<Supplier?> GetByIdAsync(int supplierId);

    Task<List<Supplier>> GetAllAsync();

    Task AddAsync(Supplier supplier);

    Task UpdateAsync(Supplier supplier);

    Task DeactivateAsync(int supplierId);
}