using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.News
{
	public class BlogDetails : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
