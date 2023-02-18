using EndProject.DAL;
using EndProject.Models.AllTourInfo;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class ContinentController : Controller
    {
        AppDbContext _context { get; }
        public ContinentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Continents.ToList());
        }
        public IActionResult Delete(int id)
        {
            Continent continent = _context.Continents.Find(id);
            if (continent is null) return NotFound();
            _context.Continents.Remove(continent);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Continent continent)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Continents.Add(continent);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            Continent continent = _context.Continents.Find(id);
            if (continent is null) return NotFound();
            return View(continent);
        }
        [HttpPost]
        public IActionResult Update(int? id, Continent continent)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null || id != continent.Id) return BadRequest();
            Continent exist = _context.Continents.Find(id);
            if (exist is null) return NotFound();
            exist.Name = continent.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
