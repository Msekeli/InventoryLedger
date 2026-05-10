using Inventory.Domain.Entities;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Services;

public class SaleService : ISaleService
{
    public Task<Sale> ProcessSaleAsync(
        List<SaleLine> saleLines,
        int processedByUserId)
    {
        throw new NotImplementedException();
    }

    public Task<Sale?> GetByIdAsync(int saleId)
    {
        throw new NotImplementedException();
    }

    public Task<List<Sale>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}