using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                MostFrequents=_context.MostFrequents.ToList(),
                QuestionCategories = _context.QuestionCategories.ToList(),
                Questions = _context.Questions.Include(q => q.QuestionCategory).ToList()
            };

            return View(home);
        }
    }
}
