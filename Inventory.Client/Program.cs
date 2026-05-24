using Inventory.Client;
using Inventory.Client.Services.Items;
using Inventory.Client.Services.Transactions;
using Inventory.Client.Services.Inventory;
using Inventory.Client.Services.Dashboard;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Root app
builder.RootComponents.Add<App>("#app");

// HttpClient
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5222/")
});

// Existing services
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IStockTransactionService, StockTransactionService>();
builder.Services.AddScoped<IInventoryServiceClient, InventoryServiceClient>();
builder.Services.AddScoped<DashboardService>();

// Radzen services
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<ContextMenuService>();

await builder.Build().RunAsync();