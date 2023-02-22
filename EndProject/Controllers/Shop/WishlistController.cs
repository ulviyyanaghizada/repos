using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Shop
{
    public class WishlistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
