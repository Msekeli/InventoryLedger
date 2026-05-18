using Bogus;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Infrastructure.Persistence;

public static class InventoryDbContextSeed
{
    public static async Task SeedAsync(
        InventoryDbContext db)
    {
        Randomizer.Seed =
            new Random(2026);

        db.StockTransactions.RemoveRange(
            db.StockTransactions);

        db.Items.RemoveRange(
            db.Items);

        db.AppUsers.RemoveRange(
            db.AppUsers);

        db.AppRoles.RemoveRange(
            db.AppRoles);

        db.Suppliers.RemoveRange(
            db.Suppliers);

        await db.SaveChangesAsync();

        // Roles
        var roles = new List<AppRole>
        {
            new() { Name = "Owner" },
            new() { Name = "Cashier" },
            new() { Name = "Stock Clerk" }
        };

        await db.AppRoles.AddRangeAsync(
            roles);

        await db.SaveChangesAsync();

        // Users
        var users =
            new Faker<AppUser>()
                .RuleFor(
                    u => u.FirstName,
                    f => f.Name.FirstName())
                .RuleFor(
                    u => u.LastName,
                    f => f.Name.LastName())
                .RuleFor(
                    u => u.IsActive,
                    true)
                .RuleFor(
                    u => u.AppRoleId,
                    f => f.PickRandom(
                        roles).Id)
                .Generate(5);

        await db.AppUsers.AddRangeAsync(
            users);

        await db.SaveChangesAsync();

        // Suppliers
        var suppliers =
            new Faker<Supplier>()
                .RuleFor(
                    s => s.SupplierCode,
                    f => $"SUP-{f.IndexFaker + 1:000}")
                .RuleFor(
                    s => s.Name,
                    f => f.Company.CompanyName())
                .RuleFor(
                    s => s.ContactPerson,
                    f => f.Name.FullName())
                .RuleFor(
                    s => s.PhoneNumber,
                    f => f.Phone.PhoneNumber())
                .RuleFor(
                    s => s.EmailAddress,
                    f => f.Internet.Email())
                .RuleFor(
                    s => s.IsActive,
                    true)
                .Generate(10);

        await db.Suppliers.AddRangeAsync(
            suppliers);

        await db.SaveChangesAsync();

        var supplierIds =
            suppliers
                .Select(s => s.Id)
                .ToList();

        // Items
        var items =
            new Faker<Item>()
                .RuleFor(
                    i => i.SKU,
                    f => $"SKU-{f.IndexFaker + 1:000}")
                .RuleFor(
                    i => i.Name,
                    f => f.Commerce.ProductName())
                .RuleFor(
                    i => i.Description,
                    f => f.Commerce.ProductDescription())
                .RuleFor(
                    i => i.CostPrice,
                    f => decimal.Parse(
                        f.Commerce.Price(5, 50)))
                .RuleFor(
                    i => i.SellingPrice,
                    (f, i) =>
                        i.CostPrice +
                        f.Random.Decimal(2, 15))
                .RuleFor(
                    i => i.LowStockThreshold,
                    f => f.Random.Int(5, 15))
                .RuleFor(
                    i => i.IsActive,
                    true)
                .RuleFor(
                    i => i.SupplierId,
                    f => f.PickRandom(
                        supplierIds))
                .Generate(30);

        await db.Items.AddRangeAsync(
            items);

        await db.SaveChangesAsync();

        var userIds =
            users
                .Select(u => u.Id)
                .ToList();

        // Ledger transactions
        var transactions =
            new List<StockTransaction>();

        var random =
            new Random(2026);

        foreach (var item in items)
        {
            // Starting stock
            int currentStock =
                random.Next(30, 80);

            transactions.Add(
                new StockTransaction
                {
                    ItemId = item.Id,
                    Quantity = currentStock,
                    TransactionType =
                        TransactionType.StockReceived,
                    Timestamp =
                        DateTime.UtcNow.AddDays(-30),
                    ReferenceNumber =
                        $"OPEN-{item.Id}",
                    Notes =
                        "Opening stock",
                    PerformedByUserId =
                        random.Next(1, 6)
                });

            // 6–9 movements per item
            int movements =
                random.Next(6, 10);

            for (
                int i = 0;
                i < movements;
                i++)
            {
                bool restock =
                    random.NextDouble() < 0.25;

                if (restock)
                {
                    int qty =
                        random.Next(10, 30);

                    currentStock += qty;

                    transactions.Add(
                        new StockTransaction
                        {
                            ItemId = item.Id,
                            Quantity = qty,
                            TransactionType =
                                TransactionType.StockReceived,
                            Timestamp =
                                DateTime.UtcNow.AddDays(
                                    -random.Next(1, 29)),
                            ReferenceNumber =
                                $"REC-{item.Id}-{i}",
                            Notes =
                                "Supplier stock",
                            PerformedByUserId =
                                random.Next(1, 6)
                        });

                    continue;
                }

                if (currentStock <= 5)
                    continue;

                int saleQty =
                    random.Next(
                        1,
                        Math.Min(
                            currentStock,
                            8));

                currentStock -= saleQty;

                transactions.Add(
                    new StockTransaction
                    {
                        ItemId = item.Id,
                        Quantity = saleQty,
                        TransactionType =
                            TransactionType.Sale,
                        Timestamp =
                            DateTime.UtcNow.AddDays(
                                -random.Next(1, 29)),
                        ReferenceNumber =
                            $"SAL-{item.Id}-{i}",
                        Notes =
                            "Customer sale",
                        PerformedByUserId =
                            random.Next(1, 6)
                    });
            }
        }

        await db.StockTransactions
            .AddRangeAsync(
                transactions);

        await db.SaveChangesAsync();
    }
}