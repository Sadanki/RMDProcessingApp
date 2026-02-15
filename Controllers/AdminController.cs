using Microsoft.AspNetCore.Mvc;
using RMDProcessingApp.Repositories;

namespace RMDProcessingApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISystemConfigurationRepository _configRepository;

        public AdminController(
            IUserRepository userRepository,
            ISystemConfigurationRepository configRepository)
        {
            _userRepository = userRepository;
            _configRepository = configRepository;
        }

        private string? CurrentRole => HttpContext.Session.GetString("CurrentUserRole");

        public IActionResult Dashboard()
        {
            if (CurrentRole != "Admin")
                return Forbid();

            var users = _userRepository.GetAll();
            var configs = _configRepository.GetAll();
            ViewBag.Configs = configs;
            return View(users);
        }
    }
}
