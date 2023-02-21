using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers.Home
{
    public class HomeController : Controller
    {
        readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM { 

                Entries = _context.Entries , 
                Chooses = _context.Chooses.ToList(),
                Agencies=_context.Agencies,
                Brands=_context.Brands,
                Products=_context.Products.Include(p=>p.ProductImages)
            
            };

            return View(home);
        }
       
    }
}
