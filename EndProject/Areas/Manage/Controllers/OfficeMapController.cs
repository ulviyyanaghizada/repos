using EndProject.DAL;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class OfficeMapController : Controller
    {
        readonly AppDbContext _context;
        public OfficeMapController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.OfficeMaps.ToList());
        }
        public IActionResult Delete(int id)
        {

            OfficeMap office = _context.OfficeMaps.Find(id);
            if (office is null)
            {
                return NotFound();
            }
            _context.OfficeMaps.Remove(office);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(OfficeMap office)
        {
            if (_context.OfficeMaps.ToList().Count >= 3) return RedirectToAction(nameof(Index));

            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.OfficeMaps.Add(office);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            OfficeMap office = _context.OfficeMaps.Find(id);
            if (office is null) return NotFound();
            return View(office);
        }
        [HttpPost]
        public IActionResult Update(int? id, OfficeMap office)
        {
            if (id is null || id == 0) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }
            OfficeMap exist = _context.OfficeMaps.FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();

            exist.Description = office.Description;
            exist.Title = office.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
