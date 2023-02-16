using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.ViewModels;
using EndProject.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BrandController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public BrandController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.MaxPageCount = Math.Ceiling((decimal)_context.Brands.Count() / 5);
            ViewBag.CurrentPage = page;
            return View(_context.Brands.ToList().Skip((page - 1) * 5).Take(5).ToList());
            
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBrandVM createBrand)
        {
            var image = createBrand.Image;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            Brand brand = new Brand()
            {

                ImageUrl = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images","brand")),

            };
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Brand exist = _context.Brands.FirstOrDefault(b => b.Id == id);
            if (exist is null) return NotFound();
            exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/brand");
            _context.Brands.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Brand exist = _context.Brands.FirstOrDefault(b => b.Id == id);
            if (exist is null) return NotFound();
            UpdateBrandVM update = new UpdateBrandVM()
            {
                ImageUrl = exist.ImageUrl

            };
            ViewBag.Image = exist.ImageUrl;
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateBrandVM update)
        {
            if (id is null || id == 0) return BadRequest();
            var image = update.Image;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Image = _context.Brands.FirstOrDefault(b => b.Id == id).ImageUrl;
                return View();
            }
            Brand exist = _context.Brands.FirstOrDefault(b => b.Id == id);
            if (exist is null) return NotFound();

            if (image != null)
            {
                string newImage = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "brand"));
                exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/brand");
                exist.ImageUrl = newImage;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
