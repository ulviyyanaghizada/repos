using EndProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers.Pages
{
    public class Destinations2 : Controller
    {
        readonly AppDbContext _context;

        public Destinations2(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {

            var tours = _context.Countries.Include(c=>c.Tours).ThenInclude(c=>c.TourImages)
                .Include(c=>c.Tours).ThenInclude(c => c.TourDays).FirstOrDefault(x=>x.Id==id);
            return View(tours);
        }
    }
}
