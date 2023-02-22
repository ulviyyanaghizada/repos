using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.News
{
	public class BlogClassicController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
