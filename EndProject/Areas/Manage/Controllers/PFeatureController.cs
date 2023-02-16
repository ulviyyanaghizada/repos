using EndProject.DAL;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PFeatureController : Controller
    {
        AppDbContext _context { get; }
        public PFeatureController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.PFeatures.ToList());
        }
        public IActionResult Delete(int id)
        {
            PFeature feature = _context.PFeatures.Find(id);
            if (feature is null) return NotFound();
            _context.PFeatures.Remove(feature);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PFeature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.PFeatures.Add(feature);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            PFeature feature = _context.PFeatures.Find(id);
            if (feature is null) return NotFound();
            return View(feature);
        }
        [HttpPost]
        public IActionResult Update(int? id, PFeature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null || id != feature.Id) return BadRequest();
            PFeature exist = _context.PFeatures.Find(id);
            if (exist is null) return NotFound();
            exist.Title = feature.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
