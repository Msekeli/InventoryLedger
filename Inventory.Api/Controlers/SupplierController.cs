using Inventory.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController
    : ControllerBase
{
    private readonly
        InventoryDbContext _db;

    public SuppliersController(
        InventoryDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult>
        GetAll()
    {
        var suppliers =
            await _db.Suppliers
                .Where(s => s.IsActive)
                .OrderBy(s => s.Name)
                .Select(s => new
                {
                    s.Id,
                    s.SupplierCode,
                    s.Name,
                    s.ContactPerson,
                    s.PhoneNumber,
                    s.EmailAddress
                })
                .ToListAsync();

        return Ok(suppliers);
    }
}