using EndProject.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers.Pages
{
    public class Tours : Controller
    {
        readonly AppDbContext _context;

        public Tours(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            var tour = _context.Tours.Include(t => t.TourDays).ThenInclude(t=>t.TourDaysImages).Include(t=>t.TourDays).ThenInclude(t=>t.Hotel).ThenInclude(r=>r.HotelRooms).ThenInclude(r=>r.Room).Include(t => t.TourCategories).ThenInclude(t=>t.TCategory)
                .Include(t=>t.TourFeatures).ThenInclude(t=>t.TFeature).Include(t => t.TourFacilities).ThenInclude(t => t.TFacilitie).Include(t=>t.TourImages).FirstOrDefault(x => x.Id == id);
            return View(tour);
        }
    }
}
