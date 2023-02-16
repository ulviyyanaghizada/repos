using EndProject.DAL;
using EndProject.Models.ViewModels;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;
using EndProject.Utilities.Extensions;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AgencyController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public AgencyController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.MaxPageCount = Math.Ceiling((decimal)_context.Agencies.Count() / 5);
            ViewBag.CurrentPage = page;
            return View(_context.Agencies.ToList().Skip((page - 1) * 5).Take(5).ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateAgencyVM createAgency)
        {
            if (_context.Agencies.ToList().Count >= 1) return RedirectToAction(nameof(Index));

            var image = createAgency.Image;
            var imageCover = createAgency.ImageCover;
            string result = image?.CheckValidate("image/", 600);

            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }
            result = imageCover?.CheckValidate("image/", 600);

            if (result?.Length > 0)
            {
                ModelState.AddModelError("ImageCover", result);
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            Agency agency = new Agency()
            {

                Description = createAgency.Description,
                FirstTitle = createAgency.FirstTitle,
                SecondTitle=createAgency.SecondTitle,
                ThirdTitle=createAgency.ThirdTitle,
                Video=createAgency.Video,
                ImageUrl = image?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images")),
                ImageCoverUrl = imageCover?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images")),

            };
            _context.Agencies.Add(agency);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Agency exist = _context.Agencies.FirstOrDefault(a => a.Id == id);
            if (exist is null) return NotFound();
            exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
            exist.ImageCoverUrl.DeleteFile(_env.WebRootPath, "assets/images");
            _context.Agencies.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Agency exist = _context.Agencies.FirstOrDefault(a => a.Id == id);
            if (exist is null) return NotFound();
            UpdateAgencyVM update = new UpdateAgencyVM()
            {
                FirstTitle = exist.FirstTitle,
                SecondTitle=exist.SecondTitle,
                ThirdTitle=exist.ThirdTitle,
                Video=exist.Video,
                Description = exist.Description,
                ImageUrl = exist.ImageUrl,
                ImageCoverUrl=exist.ImageCoverUrl

            };
            ViewBag.Image = exist.ImageUrl;
            ViewBag.ImageCover = exist.ImageCoverUrl;
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateAgencyVM update)
        {
            if (id is null || id == 0) return BadRequest();
            var image = update.Image;
            var imageCover=update.ImageCover;
            var result = image?.CheckValidate("image/", 600);
            var result1 = imageCover?.CheckValidate("image/", 600);

            if (result?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }
            if (result1?.Length > 0)
            {
                ModelState.AddModelError("Image", result);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Image = _context.Agencies.FirstOrDefault(a => a.Id == id).ImageUrl;
                ViewBag.ImageCover = _context.Agencies.FirstOrDefault(a => a.Id == id).ImageCoverUrl;
                return View();
            }
            Agency exist = _context.Agencies.FirstOrDefault(a => a.Id == id);
            if (exist is null) return NotFound();

            if (image != null)
            {
                string newImage = image.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images"));
                exist.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
                exist.ImageUrl = newImage;
            }

            if (imageCover != null)
            {
                string newImageCover = imageCover.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images"));
                exist.ImageCoverUrl.DeleteFile(_env.WebRootPath, "assets/images");
                exist.ImageCoverUrl = newImageCover;
            }

            exist.FirstTitle = update.FirstTitle;
            exist.SecondTitle= update.SecondTitle;
            exist.ThirdTitle= update.ThirdTitle;
            exist.Video=update.Video;
            exist.Description = update.Description;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
