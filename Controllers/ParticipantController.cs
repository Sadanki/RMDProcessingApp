using Microsoft.AspNetCore.Mvc;
using RMDProcessingApp.Models;
using RMDProcessingApp.Repositories;

namespace RMDProcessingApp.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantRepository _repository;

        public ParticipantController(IParticipantRepository repository)
        {
            _repository = repository;
        }

        private string? CurrentRole => HttpContext.Session.GetString("CurrentUserRole");

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        // List: read-only, all roles
        public IActionResult List()
        {
            var participants = _repository.GetAll();
            return View(participants);
        }

        // Summary: read-only, all roles
        public IActionResult Summary(int id)
        {
            var participant = _repository.GetById(id);
            if (participant == null) return NotFound();
            return View(participant);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (CurrentRole is not ("Admin" or "Processor"))
                return Forbid();

            return View(new Participant
            {
                ParticipantStatus = "Active",
                PlanType = "PLAN-401K",
                EmploymentStatus = "Active",
                DateOfBirth = DateTime.Today.AddYears(-73)
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Participant participant)
        {
            if (CurrentRole is not ("Admin" or "Processor"))
                return Forbid();

            if (ModelState.IsValid)
            {
                _repository.Add(participant);
                return RedirectToAction("List");
            }

            return View(participant);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (CurrentRole is not ("Admin" or "Processor"))
                return Forbid();

            var participant = _repository.GetById(id);
            if (participant == null) return NotFound();
            return View(participant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Participant participant)
        {
            if (CurrentRole is not ("Admin" or "Processor"))
                return Forbid();

            if (id != participant.ParticipantId) return BadRequest();

            if (ModelState.IsValid)
            {
                _repository.Update(participant);
                return RedirectToAction("List");
            }

            return View(participant);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (CurrentRole is not ("Admin" or "Processor"))
                return Forbid();

            var participant = _repository.GetById(id);
            if (participant == null) return NotFound();
            return View(participant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (CurrentRole is not ("Admin" or "Processor"))
                return Forbid();

            _repository.Delete(id);
            return RedirectToAction("List");
        }
    }
}
