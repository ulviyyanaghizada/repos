using EndProject.DAL;
using EndProject.Models.AllTourInfo;
using EndProject.Models.ViewModels;
using EndProject.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TrFacilitieController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public TrFacilitieController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.TrFacilities.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTrFacilitieVM create)
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
            TrFacilitie facilitie = new TrFacilitie()
            {

                Title = create.Title,
                IconUrl = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "resource")),

            };
            _context.TrFacilities.Add(facilitie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            TrFacilitie exist = _context.TrFacilities.FirstOrDefault(f => f.Id == id);
            if (exist is null) return NotFound();
            exist.IconUrl.DeleteFile(_env.WebRootPath, "assets/images/resource");
            _context.TrFacilities.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            TrFacilitie exist = _context.TrFacilities.FirstOrDefault(f => f.Id == id);
            if (exist is null) return NotFound();
            UpdateTrFacilitieVM update = new UpdateTrFacilitieVM()
            {
                Title = exist.Title,
                IconUrl = exist.IconUrl

            };
            ViewBag.Icon = exist.IconUrl;
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateTrFacilitieVM update)
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
                ViewBag.Icon = _context.TrFacilities.FirstOrDefault(c => c.Id == id).IconUrl;
                return View();
            }
            TrFacilitie exist = _context.TrFacilities.FirstOrDefault(f => f.Id == id);
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
