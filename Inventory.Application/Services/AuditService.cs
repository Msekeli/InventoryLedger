using Inventory.Domain.Entities;
using Inventory.Application.Interfaces;

namespace Inventory.Application.Services;

public class AuditService : IAuditService
{
    public Task RecordAsync(AuditLog auditLog)
    {
        throw new NotImplementedException();
    }

    public Task<AuditLog?> GetByIdAsync(int auditLogId)
    {
        throw new NotImplementedException();
    }

    public Task<List<AuditLog>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}