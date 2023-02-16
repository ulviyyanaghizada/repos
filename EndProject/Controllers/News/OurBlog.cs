using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.News
{
    public class OurBlog : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
