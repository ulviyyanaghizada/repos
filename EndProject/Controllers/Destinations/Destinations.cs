using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers.Destinations
{
    public class Destinations : Controller
    {

        readonly AppDbContext _context;

        public Destinations(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            DestinationVM destinationVM = new DestinationVM
            {
                Continents=_context.Continents.ToList(),
                Countires= _context.Countries.Include(c=>c.Tours).ToList(),
            };
            return View(destinationVM);
        }
    }
}
