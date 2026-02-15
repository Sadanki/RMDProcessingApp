using Microsoft.AspNetCore.Mvc;
using RMDProcessingApp.Repositories;

namespace RMDProcessingApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly IRmdRepository _rmdRepository;
        private readonly IAuditLogRepository _auditLogRepository;

        public ReportController(
            IRmdRepository rmdRepository,
            IAuditLogRepository auditLogRepository)
        {
            _rmdRepository = rmdRepository;
            _auditLogRepository = auditLogRepository;
        }

        private string? CurrentRole => HttpContext.Session.GetString("CurrentUserRole");

        // Reports dashboard – Admin + Viewer
        public IActionResult Dashboard()
        {
            if (CurrentRole is not ("Admin" or "Viewer"))
                return Forbid();

            // In a real app you'd get all RMDs; here just sample
            // Assuming a method GetAll() exists; if not, adjust accordingly.
            var allRmds = _rmdRepository.GetByParticipant(1001); // demo
            return View(allRmds);
        }

        // Audit logs – Admin only
        public IActionResult AuditLogs()
        {
            if (CurrentRole != "Admin")
                return Forbid();

            var logs = _auditLogRepository.GetAll();
            return View(logs);
        }
    }
}
