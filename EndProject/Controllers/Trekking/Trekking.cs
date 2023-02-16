using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Trekking
{
    public class Trekking : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
