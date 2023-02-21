using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.AllTourInfo;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class DifficultyController : Controller
    {
        AppDbContext _context { get; }
        public DifficultyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Difficulties.ToList());
        }
        public IActionResult Delete(int id)
        {
            Difficulty difficulty = _context.Difficulties.Find(id);
            if (difficulty is null) return NotFound();
            _context.Difficulties.Remove(difficulty);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Difficulty difficulty)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Difficulties.Add(difficulty);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            Difficulty difficulty = _context.Difficulties.Find(id);
            if (difficulty is null) return NotFound();
            return View(difficulty);
        }
        [HttpPost]
        public IActionResult Update(int? id, Difficulty difficulty)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null || id != difficulty.Id) return BadRequest();
            Difficulty exist = _context.Difficulties.Find(id);
            if (exist is null) return NotFound();
            exist.Name = difficulty.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
