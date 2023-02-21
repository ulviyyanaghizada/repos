using EndProject.DAL;
using EndProject.Models.AllTourInfo;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class TrFeatureController : Controller
    {
        AppDbContext _context { get; }
        public TrFeatureController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.TrFeatures.ToList());
        }
        public IActionResult Delete(int id)
        {
            TrFeature feature = _context.TrFeatures.Find(id);
            if (feature is null) return NotFound();
            _context.TrFeatures.Remove(feature);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TrFeature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.TrFeatures.Add(feature);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            TrFeature feature = _context.TrFeatures.Find(id);
            if (feature is null) return NotFound();
            return View(feature);
        }
        [HttpPost]
        public IActionResult Update(int? id, TrFeature feature)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null || id != feature.Id) return BadRequest();
            TrFeature exist = _context.TrFeatures.Find(id);
            if (exist is null) return NotFound();
            exist.Title = feature.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
