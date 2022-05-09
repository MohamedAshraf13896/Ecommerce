using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class NavigatorController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Cart()
        {
            return View();
        }
    }
}
