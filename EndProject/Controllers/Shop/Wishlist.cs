using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Shop
{
    public class Wishlist : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
