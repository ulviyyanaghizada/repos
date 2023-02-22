using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers
{
    public class GalleryController : Controller
    {
		readonly AppDbContext _context;

		public GalleryController(AppDbContext context)
		{
			_context = context;

		}
		public IActionResult Index()
        {
			HomeVM home = new HomeVM
			{
				Brands = _context.Brands,

			};

			return View(home);
		}
    }
}
