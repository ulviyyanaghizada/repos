using EndProject.DAL;

using EndProject.Models;
using EndProject.Models.AllTourInfo;
using EndProject.Models.ViewModels;
using EndProject.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class TourController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public TourController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var country = _context.Tours.Include(t => t.TourCategories).ThenInclude(tc => tc.TCategory).
            Include(t => t.TourFeatures).ThenInclude(tc => tc.TFeature).
            Include(t => t.TourFacilities).ThenInclude(tc => tc.TFacilitie).Include(t => t.Country).Include(t => t.TourImages);

            return View(country);
          
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Tour exist = _context.Tours.Include(t => t.TourCategories)
                .Include(t => t.TourFeatures).Include(t => t.TourFacilities)
                .Include(t => t.TourImages).FirstOrDefault(t => t.Id == id);
            if (exist is null) return NotFound();
            exist.VideoUrl.DeleteFile(_env.WebRootPath, "assets/images/tour");
            foreach (TourImage image in exist.TourImages)
            {
                image.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/tour");
            }
            _context.TourFeatures.RemoveRange(exist.TourFeatures);
            _context.TourCategories.RemoveRange(exist.TourCategories);
            _context.TourFacilities.RemoveRange(exist.TourFacilities);
            _context.TourImages.RemoveRange(exist.TourImages);
            _context.Tours.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            ViewBag.TFacilities = new SelectList(_context.TFacilities, nameof(TFacilitie.Id), nameof(TFacilitie.Title));
            ViewBag.TFeatures = new SelectList(_context.TFeatures, nameof(TFeature.Id), nameof(TFeature.Title));
            ViewBag.TCategories = new SelectList(_context.TCategories, nameof(TCategory.Id), nameof(TCategory.Name));
            ViewBag.Countries = new SelectList(_context.Countries.ToList(), nameof(Country.Id), nameof(Country.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTourVM ctour)
        {
            var video = ctour.Video;
            var result1 = video?.CheckValidate("video/", 20000);
            if (result1?.Length > 0)
            {
                ModelState.AddModelError("Video", result1);
            }


            var otherImgs = ctour.OtherImages ?? new List<IFormFile>();
            var primaryimg = ctour.PrimaryImage;
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
            foreach (int facilitieId in (ctour.TFacilitiesIds ?? new List<int>()))
            {
                if (!_context.TFacilities.Any(f => f.Id == facilitieId))
                {
                    ModelState.AddModelError("TFacilitiesIds", "There is no matched facilitie with this id!");
                    break;
                }
            }
            foreach (int tFeatureId in (ctour.TFeaturesIds ?? new List<int>()))
            {
                if (!_context.TFeatures.Any(c => c.Id == tFeatureId))
                {
                    ModelState.AddModelError("TFeaturesIds", "There is no matched feature with this id!");
                    break;
                }
            }
            foreach (int categoryId in (ctour.TCategoriesIds ?? new List<int>()))
            {
                if (!_context.TCategories.Any(c => c.Id == categoryId))
                {
                    ModelState.AddModelError("TCategoriesIds", "There is no matched category with this id!");
                    break;
                }
            }
            if (!_context.Countries.Any(p => p.Id == ctour.CountryId))
            {
                ModelState.AddModelError("CountryId", "Bu Id'li country yoxdur");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.TFacilities = new SelectList(_context.TFacilities, nameof(TFacilitie.Id), nameof(TFacilitie.Title));
                ViewBag.TFeatures = new SelectList(_context.TFeatures, nameof(TFeature.Id), nameof(TFeature.Title));
                ViewBag.TCategories = new SelectList(_context.TCategories, nameof(TCategory.Id), nameof(TCategory.Name));
                ViewBag.Countries = new SelectList(_context.Countries.ToList(), nameof(Country.Id), nameof(Country.Name));

                return View();
            }

            var facilities = _context.TFacilities.Where(f => ctour.TFacilitiesIds.Contains(f.Id));
            var categories = _context.TCategories.Where(c => ctour.TCategoriesIds.Contains(c.Id));
            var features = _context.TFeatures.Where(f => ctour.TFeaturesIds.Contains(f.Id));
            Tour tour = new Tour
            {
                Title = ctour.Title,
                Price = ctour.Price,
                Description = ctour.Description,
                Name = ctour.Name,
                GroupSize = ctour.GroupSize,
                CountryId = ctour.CountryId,
                VideoUrl = video.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images","tour"))
            };
            List<TourImage> images = new List<TourImage>();
            images.Add(new TourImage
            {
                ImageUrl = primaryimg?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "tour")),
                Tour = tour,
                IsPrimary = true
            });
            foreach (var item in otherImgs)
            {
                images.Add(
                    new TourImage
                    {
                        ImageUrl = item?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "tour")),
                        Tour = tour,
                        IsPrimary = false
                    });
            }
            tour.TourImages = images;
            _context.Tours.Add(tour);
            foreach (var item in features)
            {
                _context.TourFeatures.Add(new TourFeature { Tour = tour, TFeatureId = item.Id });
            }
            foreach (var item in facilities)
            {
                _context.TourFacilities.Add(new TourFacilitie { Tour = tour, TFacilitieId = item.Id });
            }
            foreach (var item in categories)
            {
                _context.TourCategories.Add(new TourCategory { Tour = tour, TCategoryId = item.Id });
            }
            
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            var tour = _context.Tours.Include(t => t.TourCategories).Include(t => t.TourFeatures).Include(t => t.TourFacilities)
                .Include(t => t.TourImages).Include(t => t.Country).FirstOrDefault(p => p.Id == id);
            if (tour is null) return NotFound();
            UpdateTourVM update = new UpdateTourVM
            {
                Title = tour.Title,
                Price = tour.Price,
                Description = tour.Description,
                GroupSize = tour.GroupSize,
                TourImages = tour.TourImages.ToList(),
                CountryId=tour.CountryId,
                TFeaturesIds = tour.TourFeatures.Select(pc => pc.TFeatureId).ToList(),
                TFacilitiesIds = tour.TourFacilities.Select(pc => pc.TFacilitieId).ToList(),
                CategoriesIds = tour.TourCategories.Select(pc => pc.TCategoryId).ToList(),
                VideoUrl = tour.VideoUrl,
            };
            ViewBag.TFacilities = new SelectList(_context.TFacilities, nameof(TFacilitie.Id), nameof(TFacilitie.Title));
            ViewBag.TFeatures = new SelectList(_context.TFeatures, nameof(TFeature.Id), nameof(TFeature.Title));
            ViewBag.TCategories = new SelectList(_context.TCategories, nameof(TCategory.Id), nameof(TCategory.Name));
            ViewBag.Countries = new SelectList(_context.Countries.ToList(), nameof(Country.Id), nameof(Country.Name));
            ViewBag.Video = tour.VideoUrl;

            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateTourVM update)
        {
            if (id is null || id == 0) return BadRequest();

            var video = update.Video;
            var result1 = video?.CheckValidate("video/", 2000);
            if (result1?.Length > 0)
            {
                ModelState.AddModelError("Video", result1);
            }

            foreach (int featureId in (update.TFeaturesIds ?? new List<int>()))
            {
                if (!_context.TFeatures.Any(c => c.Id == featureId))
                {
                    ModelState.AddModelError("ColorIds", "There is no matched feature with this id!");
                    break;
                }
            }
            foreach (int facilitieId in (update.TFacilitiesIds ?? new List<int>()))
            {
                if (!_context.TFacilities.Any(c => c.Id == facilitieId))
                {
                    ModelState.AddModelError("ColorIds", "There is no matched facilitie with this id!");
                    break;
                }
            }
            foreach (int categoryId in (update.CategoriesIds ?? new List<int>()))
            {
                if (!_context.TCategories.Any(c => c.Id == categoryId))
                {
                    ModelState.AddModelError("CategoriesIds", "There is no matched category with this id!");
                    break;
                }
            }
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
            if (!ModelState.IsValid)
            {
                ViewBag.TFacilities = new SelectList(_context.TFacilities, nameof(TFacilitie.Id), nameof(TFacilitie.Title));
                ViewBag.TFeatures = new SelectList(_context.TFeatures, nameof(TFeature.Id), nameof(TFeature.Title));
                ViewBag.TCategories = new SelectList(_context.TCategories, nameof(TCategory.Id), nameof(TCategory.Name));
                ViewBag.Countries = new SelectList(_context.Countries.ToList(), nameof(Country.Id), nameof(Country.Name));
                ViewBag.Video = _context.Tours.FirstOrDefault(t => t.Id == id).VideoUrl;
                return View();
            }
            var tour = _context.Tours.Include(t => t.TourFeatures).Include(t => t.TourFacilities).Include(t => t.TourCategories)
                .Include(p => p.TourImages).Include(t=>t.Country).FirstOrDefault(p => p.Id == id);
            if (tour is null) return NotFound();

            if (video != null)
            {
                string newVideo = video.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images","tour"));
                tour.VideoUrl.DeleteFile(_env.WebRootPath, "assets/images/tour");
                tour.VideoUrl = newVideo;
            }

            foreach (var item in tour.TourFacilities)
            {
                if (update.TFacilitiesIds.Contains(item.TFacilitieId))
                {
                    update.TFacilitiesIds.Remove(item.TFacilitieId);
                }
                else
                {
                    _context.TourFacilities.Remove(item);
                }
            }
            foreach (var facilitieId in update.TFacilitiesIds)
            {
                _context.TourFacilities.Add(new TourFacilitie { Tour = tour, TFacilitieId = facilitieId });
            }
            foreach (var item in tour.TourCategories)
            {
                if (update.CategoriesIds.Contains(item.TCategoryId))
                {
                    update.CategoriesIds.Remove(item.TCategoryId);
                }
                else
                {
                    _context.TourCategories.Remove(item);
                }
            }
            foreach (var categoryId in update.CategoriesIds)
            {
                _context.TourCategories.Add(new TourCategory { Tour = tour, TCategoryId = categoryId });
            }

            foreach (var item in tour.TourFeatures)
            {
                if (update.TFeaturesIds.Contains(item.TFeatureId))
                {
                    update.TFeaturesIds.Remove(item.TFeatureId);
                }
                else
                {
                    _context.TourFeatures.Remove(item);
                }
            }
            foreach (var featureId in update.TFeaturesIds)
            {
                _context.TourFeatures.Add(new TourFeature { Tour = tour, TFeatureId = featureId });
            }
            
            List<TourImage> images = new List<TourImage>();

            if (primaryImg != null)
            {
                var oldCover = tour.TourImages.FirstOrDefault(ti => ti.IsPrimary == true);
                _context.TourImages.Remove(oldCover);
                oldCover.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/tour");
                images.Add(new TourImage
                {
                    ImageUrl = primaryImg.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "tour")),
                    IsPrimary = true,
                    Tour = tour
                });
            }


            foreach (var item in otherImgs)
            {
                images.Add(new TourImage
                {
                    ImageUrl = item?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "tour")),
                    IsPrimary = null,
                    Tour = tour
                });
            }
            var delete = update.ImageIds;
            foreach (var item in (delete ?? new List<int>()))
            {
                foreach (var pi in tour.TourImages)
                {
                    if (pi.Id == item)
                    {
                        tour.TourImages.Remove(pi);
                        pi.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/tour");
                    }
                }
            }
            foreach (var image in images)
            {
                tour.TourImages.Add(image);

            }
            tour.Title = update.Title;
            tour.Name = update.Name;
            tour.Description = update.Description;
            tour.Price = update.Price;
            tour.GroupSize = update.GroupSize;
            tour.CountryId = update.CountryId;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
