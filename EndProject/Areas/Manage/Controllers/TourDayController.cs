using EndProject.DAL;
using EndProject.Models.ViewModels;
using EndProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EndProject.Utilities.Extensions;
using EndProject.Models.AllTourInfo;


namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TourDayController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public TourDayController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.TourDays.Include(t => t.Hotel).Include(t=>t.Tour).Include(p => p.TourDaysImages));
        }
        public IActionResult Create()
        {
            ViewBag.Hotels = new SelectList(_context.Hotels.ToList(), nameof(Hotel.Id), nameof(Hotel.Name));
            ViewBag.Tours = new SelectList(_context.Tours.ToList(), nameof(Tour.Id), nameof(Tour.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTourDayVM create)
        {
            var otherImgs = create.OtherImages ?? new List<IFormFile>();
            var primaryimg = create.PrimaryImage;
            string result = primaryimg.CheckValidate("image/", 600);
            if (result.Length > 0)
            {
                ModelState.AddModelError("PrimaryImage", result);
            }
            foreach (var image in otherImgs)
            {
                result = image.CheckValidate("image/", 600);
                if (result?.Length > 0)
                {
                    ModelState.AddModelError("OtherImages", result);
                }
            }
            if (!_context.Hotels.Any(p => p.Id == create.HotelId))
            {
                ModelState.AddModelError("HotelId", "Bu Id'li hotel yoxdur");
            }      
            if (!_context.Tours.Any(p => p.Id == create.TourId))
            {
                ModelState.AddModelError("TourId", "Bu Id'li tour yoxdur");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Hotels = new SelectList(_context.Hotels.ToList(), nameof(Hotel.Id), nameof(Hotel.Name));
                ViewBag.Tours = new SelectList(_context.Tours.ToList(), nameof(Tour.Id), nameof(Tour.Name));
                return View();
            }
            TourDay day = new TourDay()
            {
                Title = create.Title,
                Description = create.Description,
                HotelId = create.HotelId,
                TourId=create.TourId
            };
            List<TourDaysImage> images = new List<TourDaysImage>();
            images.Add(new TourDaysImage
            {
                ImageUrl = primaryimg?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images")),
                TourDay = day,
                IsPrimary = true
            });
            foreach (var item in otherImgs)
            {
                images.Add(
                    new TourDaysImage
                    {
                        ImageUrl = item?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images")),
                        TourDay = day,
                        IsPrimary = false
                    });
            }
            day.TourDaysImages = images;

            _context.TourDays.Add(day);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            TourDay exist = _context.TourDays.Include(t => t.Hotel).Include(t=>t.Tour).FirstOrDefault(t => t.Id == id);
            if (exist is null) return NotFound();
            UpdateTourDayVM update = new UpdateTourDayVM()
            {
                Title = exist.Title,
                Description = exist.Description,
                TourDaysImages = exist.TourDaysImages,
                HotelId = exist.HotelId,
                TourId = exist.TourId
            };
            ViewBag.Hotels = new SelectList(_context.Hotels.ToList(), nameof(Hotel.Id), nameof(Hotel.Name));
            ViewBag.Tours = new SelectList(_context.Tours.ToList(), nameof(Tour.Id), nameof(Tour.Name));
            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateTourDayVM update)
        {
            if (id is null || id == 0) return BadRequest();
            var primaryImg = update.PrimaryImage;
            var otherImgs = update.OtherImages ?? new List<IFormFile>();
            string result = primaryImg?.CheckValidate("image/", 600);
            foreach (var image in otherImgs)
            {
                result = image.CheckValidate("image/", 600);
                if (result?.Length > 0)
                {
                    ModelState.AddModelError("OtherImages", result);
                }
            }
            if (!_context.Hotels.Any(p => p.Id == update.HotelId))
            {
                ModelState.AddModelError("HotelId", "Bu Id'li hotel yoxdur");
            }
            if (!_context.Tours.Any(p => p.Id == update.TourId))
            {
                ModelState.AddModelError("TourId", "Bu Id'li tour yoxdur");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Hotels = new SelectList(_context.Hotels.ToList(), nameof(Hotel.Id), nameof(Hotel.Name));
                ViewBag.Tours = new SelectList(_context.Tours.ToList(), nameof(Tour.Id), nameof(Tour.Name));
                return View();
            }
            TourDay exist = _context.TourDays.Include(t => t.Hotel).Include(t => t.Tour).Include(t=>t.TourDaysImages).FirstOrDefault(t => t.Id == id);
            if (exist is null) return NotFound();

            List<TourDaysImage> images = new List<TourDaysImage>();

            if (primaryImg != null)
            {
                var oldCover = exist.TourDaysImages.FirstOrDefault(ti => ti.IsPrimary == true);
                _context.TourDaysImages.Remove(oldCover);
                oldCover.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
                images.Add(new TourDaysImage
                {
                    ImageUrl = primaryImg.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images")),
                    IsPrimary = true,
                    TourDay = exist
                });
            }


            foreach (var item in otherImgs)
            {
                images.Add(new TourDaysImage
                {
                    ImageUrl = item?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images")),
                    IsPrimary = null,
                    TourDay = exist
                });
            }
            var delete = update.ImageIds;
            foreach (var item in (delete ?? new List<int>()))
            {
                foreach (var pi in exist.TourDaysImages)
                {
                    if (pi.Id == item)
                    {
                        exist.TourDaysImages.Remove(pi);
                        pi.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
                    }
                }
            }
            foreach (var image in images)
            {
                exist.TourDaysImages.Add(image);

            }
            
            exist.HotelId = update.HotelId;
            exist.TourId = update.TourId;
            exist.Description = update.Description;
            exist.Title = update.Title;
       
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            TourDay exist = _context.TourDays.Include(t=>t.TourDaysImages).FirstOrDefault(t => t.Id == id);
            if (exist is null) return NotFound();
            foreach (TourDaysImage image in exist.TourDaysImages)
            {
                image.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images");
            }
            _context.TourDaysImages.RemoveRange(exist.TourDaysImages);
            _context.TourDays.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
