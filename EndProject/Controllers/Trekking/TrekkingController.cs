using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers.Trekking
{
    public class TrekkingController : Controller
    {
		readonly AppDbContext _context;

		public TrekkingController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
        {
            TrekkingVM trekking = new TrekkingVM
            {
                Difficulties = _context.Difficulties.ToList(),
                Trekkings = _context.Trekkings.Include(t=>t.Difficulty).Include(t=>t.TrekkingImages).Include(t=>t.TrekkingDays).ToList()

            };
            return View(trekking);
        }
    }
}
