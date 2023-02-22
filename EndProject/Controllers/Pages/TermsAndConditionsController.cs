using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Controllers.Pages
{
    public class TermsAndConditionsController : Controller
    {
        readonly AppDbContext _context;

        public TermsAndConditionsController(AppDbContext context)
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
