using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Shop
{
    public class OurShop : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
