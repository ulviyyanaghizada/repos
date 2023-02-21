using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers
{
    public class OurTeam : Controller
    {
        readonly AppDbContext _context;

        public OurTeam(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {
                Employees = _context.Employees.Include(e => e.Position).ToList(),
                Brands = _context.Brands
            };

            return View(home);
        }
        
    }
}
