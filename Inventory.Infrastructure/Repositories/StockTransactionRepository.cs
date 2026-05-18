using System.Data;
using Inventory.Application.Interfaces;
using Inventory.Application.Models;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class StockTransactionRepository
    : IStockTransactionRepository
{
    private readonly InventoryDbContext _context;

    public StockTransactionRepository(
        InventoryDbContext context)
    {
        _context = context;
    }

    public async Task<StockTransaction?> GetByIdAsync(int id)
    {
        return await _context.StockTransactions
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<StockTransaction>> GetByItemIdAsync(int itemId)
    {
        return await _context.StockTransactions
            .Where(t => t.ItemId == itemId)
            .ToListAsync();
    }

    public async Task<List<StockTransaction>> GetAllAsync()
    {
        return await _context.StockTransactions
            .ToListAsync();
    }

    public async Task ReceiveStockAsync(
        int itemId,
        int quantity,
        string referenceNumber,
        string notes,
        int performedByUserId)
    {
        await _context.Database.ExecuteSqlRawAsync(
            @"EXEC ReceiveStock
                @ItemId,
                @Quantity,
                @ReferenceNumber,
                @Notes,
                @PerformedByUserId",
            new SqlParameter("@ItemId", itemId),
            new SqlParameter("@Quantity", quantity),
            new SqlParameter("@ReferenceNumber", referenceNumber),
            new SqlParameter("@Notes", notes),
            new SqlParameter("@PerformedByUserId", performedByUserId));
    }

    public async Task ProcessSaleAsync(
        int itemId,
        int quantity,
        string referenceNumber,
        string notes,
        int performedByUserId)
    {
        await _context.Database.ExecuteSqlRawAsync(
            @"EXEC ProcessSale
                @ItemId,
                @Quantity,
                @ReferenceNumber,
                @Notes,
                @PerformedByUserId",
            new SqlParameter("@ItemId", itemId),
            new SqlParameter("@Quantity", quantity),
            new SqlParameter("@ReferenceNumber", referenceNumber),
            new SqlParameter("@Notes", notes),
            new SqlParameter("@PerformedByUserId", performedByUserId));
    }

    public async Task<int> GetOnHandQuantityAsync(int itemId)
    {
        using var connection =
            _context.Database.GetDbConnection();

        await connection.OpenAsync();

        using var command =
            connection.CreateCommand();

        command.CommandText =
            "GetOnHandQuantity";

        command.CommandType =
            CommandType.StoredProcedure;

        var parameter =
            command.CreateParameter();

        parameter.ParameterName =
            "@ItemId";

        parameter.Value =
            itemId;

        command.Parameters.Add(parameter);

        var result =
            await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }

    public async Task<List<InventorySummaryRow>>
        GetInventorySummaryAsync()
    {
        return await _context.Database
            .SqlQuery<InventorySummaryRow>(
                $"EXEC GetInventorySummary")
            .ToListAsync();
    }

    public async Task<List<LowStockItemRow>>
        GetLowStockItemsAsync()
    {
        return await _context.Database
            .SqlQuery<LowStockItemRow>(
                $"EXEC GetLowStockItems")
            .ToListAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}