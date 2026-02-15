using Microsoft.AspNetCore.Mvc;
using RMDProcessingApp.Models;
using RMDProcessingApp.Repositories;
using RMDProcessingApp.Services;

namespace RMDProcessingApp.Controllers
{
    public class RmdController : Controller
    {
        private readonly IParticipantRepository _participantRepository;
        private readonly IRmdRepository _rmdRepository;
        private readonly IRmdService _rmdService;
        private readonly IRmdProcessingRepository _processingRepository;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IUniformLifetimeService _uniformLifetimeService;

        public RmdController(
            IParticipantRepository participantRepository,
            IRmdRepository rmdRepository,
            IRmdService rmdService,
            IRmdProcessingRepository processingRepository,
            IAuditLogRepository auditLogRepository,
            IUniformLifetimeService uniformLifetimeService)
        {
            _participantRepository = participantRepository;
            _rmdRepository = rmdRepository;
            _rmdService = rmdService;
            _processingRepository = processingRepository;
            _auditLogRepository = auditLogRepository;
            _uniformLifetimeService = uniformLifetimeService;
        }

        private string? CurrentRole => HttpContext.Session.GetString("CurrentUserRole");

        // ===== VIEW-ONLY =====

        public IActionResult CheckEligibility(int participantId)
        {
            var participant = _participantRepository.GetById(participantId);
            if (participant == null) return NotFound();

            var age = participant.Age;
            bool eligible = age >= 73 && participant.ParticipantStatus == "Active";

            ViewBag.Participant = participant;
            ViewBag.Eligible = eligible;

            return View();
        }

        public IActionResult Details(int id)
        {
            var rmd = _rmdRepository.GetById(id);
            if (rmd == null) return NotFound();

            var participant = _participantRepository.GetById(rmd.ParticipantId);
            ViewBag.Participant = participant;

            return View(rmd);
        }

        public IActionResult History(int participantId)
        {
            var participant = _participantRepository.GetById(participantId);
            if (participant == null) return NotFound();

            var rmds = _rmdRepository.GetByParticipant(participantId);
            ViewBag.Participant = participant;
            return View(rmds);
        }

        // ===== CREATE RMD (Admin + Processor) =====

        [HttpGet]
        public IActionResult Create(int participantId)
        {
            if (CurrentRole is not ("Admin" or "Processor"))
                return Forbid();

            var participant = _participantRepository.GetById(participantId);
            if (participant == null) return NotFound();

            var year = DateTime.Today.Year;
            var model = new Rmd
            {
                ParticipantId = participantId,
                FinancialYear = year,
                OpeningBalance = 0,
                CalculatedAmount = 0,
                Status = "Draft"
            };
            ViewBag.Participant = participant;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Rmd rmd)
        {
            if (CurrentRole is not ("Admin" or "Processor"))
                return Forbid();

            var participant = _participantRepository.GetById(rmd.ParticipantId);
            if (participant == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Participant = participant;
                return View(rmd);
            }

            // Uniform Lifetime Table formula: balance ÷ factor
            var age = participant.Age;
            var factor = _uniformLifetimeService.GetLifeExpectancyFactor(age);
            rmd.CalculatedAmount = Math.Round(rmd.OpeningBalance / factor, 2);
            rmd.Status = "Draft";

            _rmdRepository.Add(rmd);
            return RedirectToAction("Details", new { id = rmd.RmdId });
        }

        // ===== APPROVE (Admin only) =====

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Approve(int id)
        {
            if (CurrentRole != "Admin")
                return Forbid();

            var rmd = _rmdRepository.GetById(id);
            if (rmd == null) return NotFound();

            if (rmd.Status == "Draft")
            {
                rmd.Status = "Approved";
                rmd.ApprovedDate = DateTime.UtcNow;
                _rmdRepository.Update(rmd);

                _auditLogRepository.Add(new AuditLog
                {
                    EntityName = "Rmd",
                    EntityId = rmd.RmdId.ToString(),
                    ActionPerformed = "ApproveRmd",
                    OldValue = "Draft",
                    NewValue = "Approved",
                    PerformedBy = HttpContext.Session.GetString("CurrentUserEmail") ?? "system"
                });
            }

            return RedirectToAction("Details", new { id });
        }

        // ===== TURN 1 (Admin + Processor) =====

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessTurnOne(int id)
        {
            if (CurrentRole is not ("Admin" or "Processor"))
                return Forbid();

            var rmd = _rmdRepository.GetById(id);
            if (rmd == null) return NotFound();

            if (rmd.Status != "Approved")
                return BadRequest("RMD must be Approved to start Turn 1.");

            rmd.Status = "ProcessingTurn1";
            _rmdRepository.Update(rmd);

            var processing = new RmdProcessing
            {
                RmdId = rmd.RmdId,
                TurnNumber = 1,
                ProcessingStartTime = DateTime.UtcNow,
                ProcessingStatus = "Running"
            };
            _processingRepository.Add(processing);

            _auditLogRepository.Add(new AuditLog
            {
                EntityName = "Rmd",
                EntityId = rmd.RmdId.ToString(),
                ActionPerformed = "ProcessTurnOne",
                OldValue = "Approved",
                NewValue = "ProcessingTurn1",
                PerformedBy = HttpContext.Session.GetString("CurrentUserEmail") ?? "system"
            });

            return RedirectToAction("Details", new { id });
        }

        // ===== TURN 2 (Admin only) =====

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessTurnTwo(int id)
        {
            if (CurrentRole != "Admin")
                return Forbid();

            var rmd = _rmdRepository.GetById(id);
            if (rmd == null) return NotFound();

            if (rmd.Status != "ProcessingTurn1")
                return BadRequest("RMD must be in ProcessingTurn1 to start Turn 2.");

            rmd.Status = "ProcessingTurn2";
            _rmdRepository.Update(rmd);

            var processing = new RmdProcessing
            {
                RmdId = rmd.RmdId,
                TurnNumber = 2,
                ProcessingStartTime = DateTime.UtcNow,
                ProcessingStatus = "Running"
            };
            _processingRepository.Add(processing);

            _auditLogRepository.Add(new AuditLog
            {
                EntityName = "Rmd",
                EntityId = rmd.RmdId.ToString(),
                ActionPerformed = "ProcessTurnTwo",
                OldValue = "ProcessingTurn1",
                NewValue = "ProcessingTurn2",
                PerformedBy = HttpContext.Session.GetString("CurrentUserEmail") ?? "system"
            });

            return RedirectToAction("Details", new { id });
        }

        // ===== STOP (Admin + Processor) =====

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StopRmd(int id, string reason)
        {
            if (CurrentRole is not ("Admin" or "Processor"))
                return Forbid();

            var rmd = _rmdRepository.GetById(id);
            if (rmd == null) return NotFound();

            var oldStatus = rmd.Status;
            rmd.Status = "Stopped";
            _rmdRepository.Update(rmd);

            _auditLogRepository.Add(new AuditLog
            {
                EntityName = "Rmd",
                EntityId = rmd.RmdId.ToString(),
                ActionPerformed = "StopRmd",
                OldValue = oldStatus,
                NewValue = $"Stopped: {reason}",
                PerformedBy = HttpContext.Session.GetString("CurrentUserEmail") ?? "system"
            });

            return RedirectToAction("Details", new { id });
        }
    }
}
