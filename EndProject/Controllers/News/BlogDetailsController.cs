using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.News
{
	public class BlogDetailsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
