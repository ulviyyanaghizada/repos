using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Pages
{
    public class Booking : Controller
    {
        readonly AppDbContext _context;

        public Booking(AppDbContext context)
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
