using EndProject.DAL;
using EndProject.Models.AllTourInfo;
using EndProject.Models.ViewModels;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class TrekkingDayController : Controller
    {
        AppDbContext _context { get; }
        public TrekkingDayController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
             return View(_context.TrekkingDays.Include(t => t.Trekking));
        }
        public IActionResult Create()
        {
            ViewBag.Trekkings = new SelectList(_context.Trekkings.ToList(), nameof(Trekking.Id), nameof(Trekking.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(TrekkingDay create)
        {
            
            if (!_context.Trekkings.Any(p => p.Id == create.TrekkingId))
            {
                ModelState.AddModelError("TrekkingId", "Bu Id'li Trekking yoxdur");
            }
            
            if (!ModelState.IsValid)
            {
                ViewBag.Trekkings = new SelectList(_context.Trekkings.ToList(), nameof(Trekking.Id), nameof(Trekking.Name));
                return View();
            }
            TrekkingDay day = new TrekkingDay()
            {
                Description = create.Description,
                TrekkingId = create.TrekkingId
                
            };
            
            _context.TrekkingDays.Add(day);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            TrekkingDay exist = _context.TrekkingDays.Include(t => t.Trekking).FirstOrDefault(t => t.Id == id);
            if (exist is null) return NotFound();
            TrekkingDay update = new TrekkingDay()
            {
           
                Description = exist.Description,
                TrekkingId = exist.TrekkingId
            };
            ViewBag.Trekkings = new SelectList(_context.Trekkings.ToList(), nameof(Trekking.Id), nameof(Trekking.Name));
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, TrekkingDay update)
        {
            if (id is null || id == 0) return BadRequest();
            if (!_context.Trekkings.Any(p => p.Id == update.TrekkingId))
            {
                ModelState.AddModelError("TrekkingId", "Bu Id'li Trekking yoxdur");
            }
           

            if (!ModelState.IsValid)
            {
                ViewBag.Trekkings = new SelectList(_context.Trekkings.ToList(), nameof(Trekking.Id), nameof(Trekking.Name));

                return View();
            }
            TrekkingDay exist = _context.TrekkingDays.Include(t => t.Trekking).FirstOrDefault(t => t.Id == id);
            if (exist is null) return NotFound();

            exist.TrekkingId = update.TrekkingId;
            exist.Description = update.Description;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            TrekkingDay exist = _context.TrekkingDays.FirstOrDefault(t => t.Id == id);
            if (exist is null) return NotFound();
            _context.TrekkingDays.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
