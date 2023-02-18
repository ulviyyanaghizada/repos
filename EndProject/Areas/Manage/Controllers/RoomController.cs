using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.AllTourInfo;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class RoomController : Controller
    {
        AppDbContext _context { get; }
        public RoomController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Rooms.ToList());
        }
        public IActionResult Delete(int id)
        {
            Room room = _context.Rooms.Find(id);
            if (room is null) return NotFound();
            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Room room)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Rooms.Add(room);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            Room room = _context.Rooms.Find(id);
            if (room is null) return NotFound();
            return View(room);
        }
        [HttpPost]
        public IActionResult Update(int? id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (id is null || id != room.Id) return BadRequest();
            Room exist = _context.Rooms.Find(id);
            if (exist is null) return NotFound();
            exist.Name = room.Name;
            exist.Price = room.Price;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
