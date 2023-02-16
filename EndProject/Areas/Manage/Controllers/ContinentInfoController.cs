using EndProject.DAL;
using EndProject.Models.ViewModels;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;
using EndProject.Utilities.Extensions;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class ContinentInfoController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public ContinentInfoController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.ContinentInfos.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateContinentInfoVM createInfo)
        {
            if (_context.ContinentInfos.ToList().Count >= 3) return RedirectToAction(nameof(Index));

            var image = createInfo.Image;
            string result = image?.CheckValidate("image/", 600);

            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            ContinentInfo info = new ContinentInfo()
            {

                Name = createInfo.Name,
                Title = createInfo.Title,
                Number = createInfo.Number,
                ImageUrl = image?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images")),

            };
            _context.ContinentInfos.Add(info);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            ContinentInfo exist = _context.ContinentInfos.FirstOrDefault(c => c.Id == id);
            if (exist is null) return NotFound();
            exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
            _context.ContinentInfos.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            ContinentInfo exist = _context.ContinentInfos.FirstOrDefault(c => c.Id == id);
            if (exist is null) return NotFound();
            UpdateContinentInfoVM update = new UpdateContinentInfoVM()
            {
                Title = exist.Title,
                Name = exist.Name,
                Number = exist.Number,
                ImageUrl = exist.ImageUrl,

            };
            ViewBag.Image = exist.ImageUrl;
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateContinentInfoVM update)
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
                ViewBag.Image = _context.ContinentInfos.FirstOrDefault(c => c.Id == id).ImageUrl;
                return View();
            }
            ContinentInfo exist = _context.ContinentInfos.FirstOrDefault(c => c.Id == id);
            if (exist is null) return NotFound();

            if (image != null)
            {
                string newImage = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images"));
                exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
                exist.ImageUrl = newImage;
            }

            exist.Name = update.Name;
            exist.Number = update.Number;
            exist.Title = update.Title;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
