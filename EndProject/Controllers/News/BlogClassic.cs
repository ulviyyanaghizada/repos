using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.News
{
	public class BlogClassic : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
