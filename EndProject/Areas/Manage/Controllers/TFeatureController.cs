using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.AllTourInfo;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class TFeatureController : Controller
    {
        AppDbContext _context { get; }
        public TFeatureController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.TFeatures.ToList());
        }
        public IActionResult Delete(int id)
        {
            TFeature feature = _context.TFeatures.Find(id);
            if (feature is null) return NotFound();
            _context.TFeatures.Remove(feature);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TFeature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.TFeatures.Add(feature);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            TFeature feature = _context.TFeatures.Find(id);
            if (feature is null) return NotFound();
            return View(feature);
        }
        [HttpPost]
        public IActionResult Update(int? id, TFeature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null || id != feature.Id) return BadRequest();
            TFeature exist = _context.TFeatures.Find(id);
            if (exist is null) return NotFound();
            exist.Title = feature.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
