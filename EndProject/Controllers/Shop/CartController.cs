using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Shop
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
