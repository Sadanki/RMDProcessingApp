using System;

namespace RMDProcessingApp.Models
{
    public class AuditLog
    {
        public int AuditId { get; set; }

        public string EntityName { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;

        public string ActionPerformed { get; set; } = string.Empty;

        public string? OldValue { get; set; }
        public string? NewValue { get; set; }

        public string PerformedBy { get; set; } = string.Empty;

        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
    }
}
