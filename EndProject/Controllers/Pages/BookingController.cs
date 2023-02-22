using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Pages
{
    public class BookingController : Controller
    {
        readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {
            
                Brands = _context.Brands,
                MostAnswers = _context.MostAnswers.ToList()
            };

            return View(home);
        }
    }
}
