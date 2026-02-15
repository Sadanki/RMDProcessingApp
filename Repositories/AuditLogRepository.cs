using System.Collections.Generic;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly List<AuditLog> _logs = new();

        public void Add(AuditLog log)
        {
            log.AuditId = _logs.Count + 1;
            _logs.Add(log);
        }

        public IEnumerable<AuditLog> GetAll() => _logs;
    }
}
