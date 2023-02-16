using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Destinations
{
    public class Destinations : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
