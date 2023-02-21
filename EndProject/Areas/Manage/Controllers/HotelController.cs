using EndProject.DAL;
using EndProject.Models.ViewModels;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;
using EndProject.Utilities.Extensions;
using EndProject.Models.AllTourInfo;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class HotelController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public HotelController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Hotels?.Include(p => p.HotelRooms).ThenInclude(pc => pc.Room));
        }
        public IActionResult Create()
        {
            ViewBag.Rooms = new SelectList(_context.Rooms, nameof(Room.Id), nameof(Room.Name));

            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateHotelVM create)
        {
            var image = create.Image;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }
            foreach (int roomId in (create.RoomIds ?? new List<int>()))
            {
                if (!_context.Rooms.Any(c => c.Id == roomId))
                {
                    ModelState.AddModelError("RoomIds", "There is no matched room with this id!");
                    break;
                }
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Rooms = new SelectList(_context.Rooms, nameof(Room.Id), nameof(Room.Name));

                return View();
            }
            var rooms = _context.Rooms.Where(r => create.RoomIds.Contains(r.Id));
            Hotel hotel = new Hotel()
            {

                Description = create.Description,
                MealDescription = create.MealDescription,
                BreakFast=create.BreakFast,
                Lunch=create.Lunch,
                Supper=create.Supper,
                Name=create.Name,
                ImageUrl = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "hotel")),

            };
            foreach (var item in rooms)
            {
                _context.HotelRooms.Add(new HotelRoom { Hotel = hotel, RoomId = item.Id });
            }
            _context.Hotels.Add(hotel);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Hotel exist = _context.Hotels.Include(h=>h.HotelRooms).FirstOrDefault(h => h.Id == id);
            if (exist is null) return NotFound();
            exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/hotel");

            _context.HotelRooms.RemoveRange(exist.HotelRooms);

            _context.Hotels.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Hotel exist = _context.Hotels.Include(p => p.HotelRooms).FirstOrDefault(h => h.Id == id);
            if (exist is null) return NotFound();
            UpdateHotelVM update = new UpdateHotelVM()
            {
                BreakFast = exist.BreakFast,
                Lunch = exist.Lunch,
                Supper = exist.Supper,
                MealDescription = exist.MealDescription,
                Description = exist.Description,
                Name = exist.Name,
                ImageUrl = exist.ImageUrl,
                RoomIds = exist.HotelRooms.Select(hr => hr.RoomId).ToList()


            };
            ViewBag.Rooms = new SelectList(_context.Rooms, nameof(Room.Id), nameof(Room.Name));

            ViewBag.Image = exist.ImageUrl;
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateHotelVM update)
        {
            foreach (int roomId in (update.RoomIds ?? new List<int>()))
            {
                if (!_context.Rooms.Any(c => c.Id == roomId))
                {
                    ModelState.AddModelError("ColorIds", "There is no matched room with this id!");
                    break;
                }
            }

            if (id is null || id == 0) return BadRequest();
            var image = update.Image;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Rooms = new SelectList(_context.Rooms, nameof(Room.Id), nameof(Room.Name));

                ViewBag.Image = _context.Hotels.FirstOrDefault(h => h.Id == id).ImageUrl;
                return View();
            }
            Hotel exist = _context.Hotels.Include(p => p.HotelRooms).FirstOrDefault(h => h.Id == id);
            if (exist is null) return NotFound();

            foreach (var item in exist.HotelRooms)
            {
                if (update.RoomIds.Contains(item.RoomId))
                {
                    update.RoomIds.Remove(item.RoomId);
                }
                else
                {
                    _context.HotelRooms.Remove(item);
                }
            }
            foreach (var roomId in update.RoomIds)
            {
                _context.HotelRooms.Add(new HotelRoom { Hotel = exist, RoomId = roomId });
            }

            if (image != null)
            {
                string newImage = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "hotel"));
                exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/hotel");
                exist.ImageUrl = newImage;
            }
            exist.BreakFast = update.BreakFast;
            exist.Lunch = update.Lunch;
            exist.Supper = update.Supper;
            exist.MealDescription = update.MealDescription;
            exist.Description = update.Description;
            exist.Name = update.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
