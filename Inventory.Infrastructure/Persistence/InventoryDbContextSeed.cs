using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Infrastructure.Persistence;

public static class InventoryDbContextSeed
{
    public static async Task SeedAsync(InventoryDbContext db)
    {
        db.StockTransactions.RemoveRange(db.StockTransactions);
        db.Items.RemoveRange(db.Items);

        await db.SaveChangesAsync();

        var items = new List<Item>
        {
            new Item
            {
                SKU = "SP-001",
                Name = "Steak & Chops Spice",
                CostPrice = 20.00m,
                SellingPrice = 29.99m,
                LowStockThreshold = 20
            },
            new Item
            {
                SKU = "SP-002",
                Name = "Chicken BBQ Spice",
                CostPrice = 17.00m,
                SellingPrice = 25.50m,
                LowStockThreshold = 15
            },
            new Item
            {
                SKU = "SP-003",
                Name = "Curry Spice Medium",
                CostPrice = 15.00m,
                SellingPrice = 23.75m,
                LowStockThreshold = 15
            }
        };

        await db.Items.AddRangeAsync(items);

        await db.SaveChangesAsync();

        var random = new Random();

        var transactions = new List<StockTransaction>();

        foreach (var item in items)
        {
            int startingStock = random.Next(80, 150);

            transactions.Add(new StockTransaction
            {
                ItemId = item.Id,
                Quantity = startingStock,
                TransactionType = TransactionType.StockReceived,
                Timestamp = DateTime.UtcNow.AddDays(-30)
            });

            for (int day = 0; day < 30; day++)
            {
                DateTime dayTimestamp =
                    DateTime.UtcNow.AddDays(-day);

                if (random.NextDouble() < 0.7)
                {
                    int saleQty = random.Next(1, 8);

                    transactions.Add(new StockTransaction
                    {
                        ItemId = item.Id,
                        Quantity = saleQty,
                        TransactionType = TransactionType.Sale,
                        Timestamp =
                            dayTimestamp.AddHours(
                                random.Next(8, 18))
                    });
                }

                if (random.NextDouble() < 0.2)
                {
                    int restockQty = random.Next(10, 40);

                    transactions.Add(new StockTransaction
                    {
                        ItemId = item.Id,
                        Quantity = restockQty,
                        TransactionType =
                            TransactionType.StockReceived,
                        Timestamp =
                            dayTimestamp.AddHours(
                                random.Next(10, 16))
                    });
                }
            }
        }

        await db.StockTransactions.AddRangeAsync(
            transactions);

        await db.SaveChangesAsync();
    }
}