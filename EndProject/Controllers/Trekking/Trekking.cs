using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Trekking
{
    public class Trekking : Controller
    {
		readonly AppDbContext _context;

		public Trekking(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
            TrekkingVM trekking = new TrekkingVM
            {
                Difficulties = _context.Difficulties.ToList(),
                Trekkings = _context.Trekkings.ToList()

            };
            return View();
        }
    }
}
