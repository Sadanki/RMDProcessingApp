using System.Collections.Generic;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Repositories
{
    public interface IAuditLogRepository
    {
        void Add(AuditLog log);
        IEnumerable<AuditLog> GetAll();
    }
}
