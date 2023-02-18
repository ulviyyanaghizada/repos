using EndProject.DAL;
using EndProject.Models.ViewModels;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndProject.Models.AllTourInfo;
using EndProject.Utilities.Extensions;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class TourCountryController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public TourCountryController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Countries.Include(e => e.Continent));
        }


        public IActionResult Create()
        {
            ViewBag.Continents = new SelectList(_context.Continents.ToList(), nameof(Position.Id), nameof(Position.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTourCountryVM create)
        {
            var image = create.Image;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }
            if (!_context.Continents.Any(p => p.Id == create.ContinentId))
            {
                ModelState.AddModelError("ContinentId", "Bu Id'li continent yoxdur");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Continents = new SelectList(_context.Continents.ToList(), nameof(Position.Id), nameof(Position.Name));
                return View();
            }
            Country country = new Country()
            {
                Name = create.Name,
                Description = create.Description,
                ImageUrl = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images","country")),
                ContinentId = create.ContinentId
            };
            _context.Countries.Add(country);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Country exist = _context.Countries.Include(e => e.Continent).FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();
            UpdateTourCountryVM update = new UpdateTourCountryVM()
            {
                Name = exist.Name,
                Description = exist.Description,
                ImageUrl = exist.ImageUrl,
                ContinentId = exist.ContinentId
            };
            ViewBag.Continents = new SelectList(_context.Continents.ToList(), nameof(Position.Id), nameof(Position.Name));
            ViewBag.Image = exist.ImageUrl;
            return View(update); 
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateTourCountryVM update)
        {
            if (id is null || id == 0) return BadRequest();
            var image = update.Image;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }
            if (!_context.Continents.Any(p => p.Id == update.ContinentId))
            {
                ModelState.AddModelError("ContinentId", "Bu Id'li continent yoxdur");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Continents = new SelectList(_context.Continents.ToList(), nameof(Position.Id), nameof(Position.Name));
                ViewBag.Image = _context.Countries.FirstOrDefault(e => e.Id == id).ImageUrl;
                return View();
            }
            Country exist = _context.Countries.Include(e => e.Continent).FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();

            if (image != null)
            {
                string newImage = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images","country"));
                exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/country");
                exist.ImageUrl = newImage;
            }
            exist.Name = update.Name;
            exist.ContinentId = update.ContinentId;
            exist.Description = update.Description;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Country exist = _context.Countries.FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();
            exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/country");
            _context.Countries.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
