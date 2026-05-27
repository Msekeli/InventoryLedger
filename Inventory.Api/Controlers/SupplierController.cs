using Inventory.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory.Api.Models;
using Inventory.Domain.Entities;

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request)
    {
        if (request is null)
            return BadRequest();

        var supplier = new Supplier
        {
            SupplierCode = request.SupplierCode,
            Name = request.Name,
            ContactPerson = request.ContactPerson,
            PhoneNumber = request.PhoneNumber,
            EmailAddress = request.EmailAddress,
            IsActive = true
        };

        _db.Suppliers.Add(supplier);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = supplier.Id }, new { supplier.Id });
    }
}