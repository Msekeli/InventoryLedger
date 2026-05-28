using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;

public interface IAuditService
{
    Task RecordAsync(AuditLog auditLog);

    Task<AuditLog?> GetByIdAsync(int auditLogId);

    Task<List<AuditLog>> GetAllAsync();
}