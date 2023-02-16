using EndProject.DAL;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class MostAnswerController : Controller
    {
        readonly AppDbContext _context;
        public MostAnswerController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.MostAnswers.ToList());
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
        public IActionResult Create(MostAnswer most)
        {
            if (_context.MostAnswers.ToList().Count >= 8) return RedirectToAction(nameof(Index));
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.MostAnswers.Add(most);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            MostAnswer most = _context.MostAnswers.Find(id);
            if (most is null) return NotFound();
            return View(most);
        }
        [HttpPost]
        public IActionResult Update(int? id, MostAnswer most)
        {
            if (id is null || id == 0) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }
            MostAnswer exist = _context.MostAnswers.FirstOrDefault(m => m.Id == id);
            if (exist is null) return NotFound();
            exist.Description = most.Description;
            exist.Title = most.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
