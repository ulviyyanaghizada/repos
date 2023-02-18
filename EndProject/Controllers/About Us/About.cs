using EndProject.DAL;
using EndProject.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers.About_Us
{
    public class About : Controller
    {
        readonly AppDbContext _context;

        public About(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {
                Employees = _context.Employees.Include(e => e.Position).ToList(),
                Chooses = _context.Chooses.ToList()
            };

            return View(home);
        }
    }
}
