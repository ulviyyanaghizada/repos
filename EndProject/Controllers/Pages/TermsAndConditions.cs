using EndProject.DAL;
using EndProject.Models.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Pages
{
    public class TermsAndConditions : Controller
    {
        readonly AppDbContext _context;

        public TermsAndConditions(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            HomeVM home = new HomeVM
            {

                Conditions = _context.Conditions.ToList(),
            };

            return View(home);
        }
    }
}
