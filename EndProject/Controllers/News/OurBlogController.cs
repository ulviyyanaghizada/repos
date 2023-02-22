using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.News
{
    public class OurBlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
