using Microsoft.AspNetCore.Mvc;
using RMDProcessingApp.Models;
using RMDProcessingApp.Repositories;
using RMDProcessingApp.Services;

namespace RMDProcessingApp.Controllers
{
    public class ParticipantController : Controller
    {
        private readonly IParticipantRepository _repository;
        private readonly IRmdService _rmdService;

        public ParticipantController(IParticipantRepository repository, IRmdService rmdService)
        {
            _repository = repository;
            _rmdService = rmdService;
        }
        
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Participant participant)
        {
            // Debug: log whether we hit POST
            Console.WriteLine($"[DEBUG] Create POST hit. Name={participant?.FullName}, PlanId={participant?.PlanId}, Age={participant?.Age}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("[DEBUG] ModelState is invalid");
                foreach (var kvp in ModelState)
                {
                    var key = kvp.Key;
                    var state = kvp.Value;
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"[DEBUG] Error on '{key}': {error.ErrorMessage}");
                    }
                }
                return View(participant);
            }

            participant.RmdStatus = _rmdService.DetermineRmdStatus(participant.Age);
            _repository.Add(participant);
            Console.WriteLine("[DEBUG] Participant added, redirecting to List");
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var participants = _repository.GetAll();
            return View(participants);
        }

        public IActionResult Summary(int id)
        {
            var participant = _repository.GetById(id);
            if (participant == null)
                return NotFound();

            return View(participant);
        }
    }
}