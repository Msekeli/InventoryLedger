using Inventory.Domain.Entities;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Services;

public class SupplierService : ISupplierService
{
    public Task<Supplier?> GetByIdAsync(int supplierId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Supplier>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Supplier supplier)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Supplier supplier)
    {
        throw new NotImplementedException();
    }

    public Task DeactivateAsync(int supplierId)
    {
        throw new NotImplementedException();
    }
}