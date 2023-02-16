using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.ViewModels;
using EndProject.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ChooseController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public ChooseController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.MaxPageCount = Math.Ceiling((decimal)_context.Chooses.Count() / 5);
            ViewBag.CurrentPage = page;
            return View(_context.Chooses.ToList().Skip((page - 1) * 5).Take(5).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateChooseVM createChoose)
        {
            if (_context.Chooses.ToList().Count >= 6) return RedirectToAction(nameof(Index));
            var image = createChoose.Image;
            var result = image?.CheckValidate("image/", 600);
            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            Choose choose = new Choose()
            {
                
                Description = createChoose.Description,
                Title=createChoose.Title,
                ImageUrl = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images","resource")),
       
            };
            _context.Chooses.Add(choose);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Choose exist = _context.Chooses.FirstOrDefault(c => c.Id == id);
            if (exist is null) return NotFound();
            exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/resource");
            _context.Chooses.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Choose exist = _context.Chooses.FirstOrDefault(e => e.Id == id);
            if (exist is null) return NotFound();
            UpdateChooseVM update = new UpdateChooseVM()
            {
                Title=exist.Title,
                Description = exist.Description,
                ImageUrl = exist.ImageUrl
         
            };
            ViewBag.Image = exist.ImageUrl;
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateChooseVM update)
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
                ViewBag.Image = _context.Chooses.FirstOrDefault(c => c.Id == id).ImageUrl;
                return View();
            }
            Choose exist = _context.Chooses.FirstOrDefault(c => c.Id == id);
            if (exist is null) return NotFound();

            if (image != null)
            {
                string newImage = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images","resource"));
                exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/resource");
                exist.ImageUrl = newImage;
            }
            exist.Title = update.Title;
            exist.Description = update.Description;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
