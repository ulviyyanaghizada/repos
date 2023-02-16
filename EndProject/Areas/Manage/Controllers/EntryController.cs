using EndProject.DAL;
using EndProject.Models;

using EndProject.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class EntryController : Controller
    {
        readonly AppDbContext _context;
        public EntryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.MaxPageCount = Math.Ceiling((decimal)_context.Agencies.Count() / 5);
            ViewBag.CurrentPage = page;
            return View(_context.Entries.ToList().Skip((page - 1) * 5).Take(5).ToList());
        }
        public IActionResult Delete(int id)
        {

            Entry entry = _context.Entries.Find(id);
            if (entry is null)
            {
                return NotFound();
            }
            _context.Entries.Remove(entry);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Entry entry)
        {
            if (_context.Entries.ToList().Count >= 1) return RedirectToAction(nameof(Index));

            if (!ModelState.IsValid)
            {
                return View();
            }
           
            _context.Entries.Add(entry);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id <= 0) return BadRequest();
            Entry entry = _context.Entries.Find(id);
            if (entry is null) return NotFound();
            return View(entry);
        }
        [HttpPost]
        public IActionResult Update(int? id,Entry entry)
        {
            if (id is null || id == 0) return BadRequest();
   
            if (!ModelState.IsValid)
            {
                return View();
            }
            Entry exist = _context.Entries.FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();

            exist.Name = entry.Name;
            exist.Description = entry.Description;
            exist.Title= entry.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
