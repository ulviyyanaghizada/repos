using EndProject.DAL;
using EndProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class PositionController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public PositionController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            return View(_context.Positions.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Position createPosition)
        {
            if (_context.Positions.Any(p => p.Name == createPosition.Name))
            {
                ModelState.AddModelError("Name", "Bu Vəzifə artıq mövcuddur");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            Position Position = new Position()
            {
                Name = createPosition.Name,
            };
            _context.Positions.Add(Position);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Position exist = _context.Positions.FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();
            Position update = new Position()
            {
                Name = exist.Name
            };
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, Position update)
        {
            if (id is null || id == 0) return BadRequest();
            if (_context.Positions.Any(p => p.Name == update.Name && p.Id != update.Id))
            {
                ModelState.AddModelError("Name", "Bu Vəzifə artıq mövcuddur");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            Position exist = _context.Positions.FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();
            exist.Name = update.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Position exist = _context.Positions.FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();
            _context.Positions.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
