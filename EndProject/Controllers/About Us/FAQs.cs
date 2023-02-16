using EndProject.DAL;
using EndProject.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers
{
    public class FAQs : Controller
    {
        readonly AppDbContext _context;

        public FAQs(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {

                Brands = _context.Brands,
                MostFrequents=_context.MostFrequents.ToList()

            };

            return View(home);
        }
    }
}
