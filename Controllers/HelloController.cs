using Microsoft.AspNetCore.Mvc;
using RMDProcessingApp.Models;

namespace RMDProcessingApp.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            var model = new RmdInfo
            {
                ApplicationName = "RMD Processing Application",
                CurrentTime = DateTime.Now.ToString("hh:mm tt on MMMM dd, yyyy"),
                Status = "Running"
            };

            ViewData["Message"] = "Welcome to the RMD Processing Application";
            ViewData["Time"] = DateTime.Now.ToString("hh:mm tt on MMMM dd, yyyy");

            return View(model);
        }
    }
}
