using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Pages
{
    public class Error : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
