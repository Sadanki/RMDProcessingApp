using Microsoft.AspNetCore.Mvc;
using RMDProcessingApp.Repositories;

namespace RMDProcessingApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IParticipantRepository _participantRepository;

        public AccountController(IAccountRepository accountRepository, IParticipantRepository participantRepository)
        {
            _accountRepository = accountRepository;
            _participantRepository = participantRepository;
        }

        public IActionResult List(int participantId)
        {
            var participant = _participantRepository.GetById(participantId);
            if (participant == null) return NotFound();

            var accounts = _accountRepository.GetByParticipant(participantId);
            ViewBag.Participant = participant;
            return View(accounts);
        }
    }
}
