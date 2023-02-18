using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.AllTourInfo;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class TourCategoryController : Controller
    {
        AppDbContext _context { get; }
        public TourCategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.TCategories.ToList());
        }
        public IActionResult Delete(int id)
        {
            TCategory category = _context.TCategories.Find(id);
            if (category is null) return NotFound();
            _context.TCategories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.TCategories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            TCategory category = _context.TCategories.Find(id);
            if (category is null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(int? id, TCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null || id != category.Id) return BadRequest();
            TCategory exist = _context.TCategories.Find(id);
            if (exist is null) return NotFound();
            exist.Name = category.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
