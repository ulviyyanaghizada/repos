using EndProject.DAL;
using EndProject.Models.ViewModels;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;
using EndProject.Utilities.Extensions;
using EndProject.Models.AllTourInfo;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TFacilitieController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public TFacilitieController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.TFacilities.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTFacilitieVM create)
        {
            var image = create.Icon;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            TFacilitie facilitie = new TFacilitie()
            {

                Title = create.Title,
                IconUrl = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "resource")),

            };
            _context.TFacilities.Add(facilitie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            TFacilitie exist = _context.TFacilities.FirstOrDefault(f => f.Id == id);
            if (exist is null) return NotFound();
            exist.IconUrl.DeleteFile(_env.WebRootPath, "assets/images/resource");
            _context.TFacilities.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            TFacilitie exist = _context.TFacilities.FirstOrDefault(f => f.Id == id);
            if (exist is null) return NotFound();
            UpdateTFacilitieVM update = new UpdateTFacilitieVM()
            {
                Title = exist.Title,
                IconUrl = exist.IconUrl

            };
            ViewBag.Icon = exist.IconUrl;
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateTFacilitieVM update)
        {
            if (id is null || id == 0) return BadRequest();
            var image = update.Icon;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Icon = _context.TFacilities.FirstOrDefault(c => c.Id == id).IconUrl;
                return View();
            }
            TFacilitie exist = _context.TFacilities.FirstOrDefault(f => f.Id == id);
            if (exist is null) return NotFound();

            if (image != null)
            {
                string newImage = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "resource"));
                exist.IconUrl.DeleteFile(_env.WebRootPath, "assets/images/resource");
                exist.IconUrl = newImage;
            }
            exist.Title = update.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
