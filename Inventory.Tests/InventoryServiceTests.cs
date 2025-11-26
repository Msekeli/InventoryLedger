using Inventory.Application.Services;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Moq;


public class InventoryServiceTests
{
    [Fact]
    public async Task GetOnHandQuantityAsync_SumsTransactions()
    {
        var item = new Item { Id = 1, SKU = "T1", Name = "Test", UnitPrice = 1, LowStockThreshold = 1 };

        var transactions = new List<StockTransaction>
        {
            new StockTransaction { ItemId = 1, QuantityChange = 10 },
            new StockTransaction { ItemId = 1, QuantityChange = -3 },
            new StockTransaction { ItemId = 1, QuantityChange = 5 }
        };

        var mockItemRepo = new Mock<IItemRepository>();
        mockItemRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(item);
        mockItemRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Item>{ item });

        var mockTxRepo = new Mock<IStockTransactionRepository>();
        mockTxRepo.Setup(r => r.GetByItemIdAsync(1)).ReturnsAsync(transactions);

        var svc = new InventoryService(mockItemRepo.Object, mockTxRepo.Object);

        var onHand = await svc.GetOnHandQuantityAsync(1);

        Assert.Equal(12, onHand);
    }
}
