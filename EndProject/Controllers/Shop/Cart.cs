using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Shop
{
    public class Cart : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
