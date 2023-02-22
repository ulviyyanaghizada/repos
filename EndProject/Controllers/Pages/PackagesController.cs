using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Pages
{
	public class PackagesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
