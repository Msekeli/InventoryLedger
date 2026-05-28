using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface ISaleService
{
    Task<Sale> ProcessSaleAsync(
        List<SaleLine> saleLines,
        int processedByUserId);

    Task<Sale?> GetByIdAsync(int saleId);

    Task<List<Sale>> GetAllAsync();
}