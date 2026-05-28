using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities;

public class AuditLog
{
    public int Id { get; set; }

    public AuditActionType ActionType { get; set; }

    public string EntityName { get; set; } = string.Empty;

    public int EntityId { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public int PerformedByUserId { get; set; }
    public AppUser? PerformedBy { get; set; }
}