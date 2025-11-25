using Inventory.Client;
using Inventory.Client.Services.Items;
using Inventory.Client.Services.Transactions;
using Inventory.Client.Services.Inventory;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register the App component (required)
builder.RootComponents.Add<App>("#app");

// Register HttpClient (required)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5222/")
});

// Register your custom services
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IStockTransactionService, StockTransactionService>();
builder.Services.AddScoped<IInventoryServiceClient, InventoryServiceClient>();

await builder.Build().RunAsync();
