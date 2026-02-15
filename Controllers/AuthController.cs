using Microsoft.AspNetCore.Mvc;
using RMDProcessingApp.Repositories;

namespace RMDProcessingApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email)
        {
            var user = _userRepository.GetByEmail(email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email.");
                return View();
            }

            HttpContext.Session.SetString("CurrentUserEmail", user.Email);
            HttpContext.Session.SetString("CurrentUserRole", user.Role);
            HttpContext.Session.SetString("CurrentUserName", user.Name);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
