using EndProject.DAL;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class MostFrequentController : Controller
    {
        readonly AppDbContext _context;
        public MostFrequentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.MaxPageCount = Math.Ceiling((decimal)_context.MostFrequents.Count() / 5);
            ViewBag.CurrentPage = page;
            return View(_context.MostFrequents.ToList().Skip((page - 1) * 5).Take(5).ToList());
        }
        public IActionResult Delete(int id)
        {

            MostFrequent mostFrequent = _context.MostFrequents.Find(id);
            if (mostFrequent is null)
            {
                return NotFound();
            }
            _context.MostFrequents.Remove(mostFrequent);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MostFrequent mostFrequent)
        {
            if (_context.MostFrequents.ToList().Count >= 6) return RedirectToAction(nameof(Index));
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.MostFrequents.Add(mostFrequent);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            MostFrequent mostFrequent = _context.MostFrequents.Find(id);
            if (mostFrequent is null) return NotFound();
            return View(mostFrequent);
        }
        [HttpPost]
        public IActionResult Update(int? id, MostFrequent mostFrequent)
        {
            if (id is null || id == 0) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }
            MostFrequent exist = _context.MostFrequents.FirstOrDefault(m => m.Id == id);
            if (exist is null) return NotFound();
            exist.Description = mostFrequent.Description;
            exist.Title = mostFrequent.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
