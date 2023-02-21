using EndProject.DAL;
using EndProject.Models.AllTourInfo;
using EndProject.Models.ViewModels;
using EndProject.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]

    public class TrekkingController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public TrekkingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Trekkings.Include(t => t.TrekkingImages).Include(t => t.TrekkingDifficulties).ThenInclude(tc => tc.Difficulty).
                Include(t => t.TrekkingFeatures).ThenInclude(tc => tc.TrFeature).
                Include(t => t.TrekkingFacilities).ThenInclude(tc => tc.TrFacilitie));
        }
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Trekking exist = _context.Trekkings.Include(t => t.TrekkingDifficulties)
                .Include(t => t.TrekkingFeatures).Include(t => t.TrekkingFacilities)
                .Include(t => t.TrekkingImages).FirstOrDefault(t => t.Id == id);
            if (exist is null) return NotFound();
            exist.VideoUrl.DeleteFile(_env.WebRootPath, "assets/images/trekking");
            foreach (TrekkingImage image in exist.TrekkingImages)
            {
                image.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/trekking");
            }
            _context.TrekkingImages.RemoveRange(exist.TrekkingImages);
            _context.TrekkingFeatures.RemoveRange(exist.TrekkingFeatures);
            _context.TrekkingFacilities.RemoveRange(exist.TrekkingFacilities);
            _context.TrekkingDifficulties.RemoveRange(exist.TrekkingDifficulties);
            _context.Trekkings.Remove(exist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            ViewBag.TrFacilities = new SelectList(_context.TrFacilities, nameof(TrFacilitie.Id), nameof(TrFacilitie.Title));
            ViewBag.TrFeatures = new SelectList(_context.TrFeatures, nameof(TrFeature.Id), nameof(TrFeature.Title));
            ViewBag.Difficulties = new SelectList(_context.Difficulties, nameof(Difficulty.Id), nameof(Difficulty.Name));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTrekkingVM trekkingVM)
        {
            var video = trekkingVM.Video;
            var result1 = video?.CheckValidate("video/", 2000);
            if (result1?.Length > 0)
            {
                ModelState.AddModelError("Video", result1);
            }


            var otherImgs = trekkingVM.OtherImages ?? new List<IFormFile>();
            var primaryimg = trekkingVM.PrimaryImage;
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
            foreach (int facilitieId in (trekkingVM.TrFacilitiesIds ?? new List<int>()))
            {
                if (!_context.TrFacilities.Any(f => f.Id == facilitieId))
                {
                    ModelState.AddModelError("TrFacilitiesIds", "There is no matched facilitie with this id!");
                    break;
                }
            }
            foreach (int trFeatureId in (trekkingVM.TrFeaturesIds ?? new List<int>()))
            {
                if (!_context.TrFeatures.Any(c => c.Id == trFeatureId))
                {
                    ModelState.AddModelError("TrFeaturesIds", "There is no matched feature with this id!");
                    break;
                }
            }
            foreach (int difficultyId in (trekkingVM.DifficultiesIds ?? new List<int>()))
            {
                if (!_context.Difficulties.Any(c => c.Id == difficultyId))
                {
                    ModelState.AddModelError("DifficultiesIds", "There is no matched difficulty with this id!");
                    break;
                }
            }
           
            if (!ModelState.IsValid)
            {
                ViewBag.TrFacilities = new SelectList(_context.TrFacilities, nameof(TrFacilitie.Id), nameof(TrFacilitie.Title));
                ViewBag.TrFeatures = new SelectList(_context.TrFeatures, nameof(TrFeature.Id), nameof(TrFeature.Title));
                ViewBag.Difficulties = new SelectList(_context.Difficulties, nameof(Difficulty.Id), nameof(Difficulty.Name));

                return View();
            }

            var facilities = _context.TrFacilities.Where(f => trekkingVM.TrFacilitiesIds.Contains(f.Id));
            var difficulties = _context.Difficulties.Where(c => trekkingVM.DifficultiesIds.Contains(c.Id));
            var features = _context.TrFeatures.Where(f => trekkingVM.TrFeaturesIds.Contains(f.Id));
            Trekking trekking = new Trekking
            {
                Title = trekkingVM.Title,
                Price = trekkingVM.Price,
                Description = trekkingVM.Description,
                Name = trekkingVM.Name,
                GroupSize = trekkingVM.GroupSize,
                VideoUrl = video.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "trekking"))
            };
            List<TrekkingImage> images = new List<TrekkingImage>();
            images.Add(new TrekkingImage
            {
                ImageUrl = primaryimg?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "trekking")),
                Trekking = trekking,
                IsPrimary = true
            });
            foreach (var item in otherImgs)
            {
                images.Add(
                    new TrekkingImage
                    {
                        ImageUrl = item?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "trekking")),
                        Trekking = trekking,
                        IsPrimary = false
                    });
            }
            trekking.TrekkingImages = images;
            _context.Trekkings.Add(trekking);
            foreach (var item in features)
            {
                _context.TrekkingFeatures.Add(new TrekkingFeature { Trekking = trekking, TrFeatureId = item.Id });
            }
            foreach (var item in facilities)
            {
                _context.TrekkingFacilities.Add(new TrekkingFacilitie { Trekking = trekking, TrFacilitieId = item.Id });
            }
            foreach (var item in difficulties)
            {
                _context.TrekkingDifficulties.Add(new TrekkingDifficulty { Trekking = trekking, DifficultyId = item.Id });
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            var trekking = _context.Trekkings.Include(t => t.TrekkingDifficulties).Include(t => t.TrekkingFacilities).Include(t => t.TrekkingFeatures)
                .Include(t => t.TrekkingImages).FirstOrDefault(p => p.Id == id);
            if (trekking is null) return NotFound();
            UpdateTrekkingVM update = new UpdateTrekkingVM
            {
                Title = trekking.Title,
                Price = trekking.Price,
                Description = trekking.Description,
                GroupSize = trekking.GroupSize,
                TrekkingImages = trekking.TrekkingImages.ToList(),
                TrFeaturesIds = trekking.TrekkingFeatures.Select(pc => pc.TrFeatureId).ToList(),
                TrFacilitiesIds = trekking.TrekkingFacilities.Select(pc => pc.TrFacilitieId).ToList(),
                DifficultiesIds = trekking.TrekkingDifficulties.Select(pc => pc.DifficultyId).ToList(),
                VideoUrl = trekking.VideoUrl,
            };
            ViewBag.TrFacilities = new SelectList(_context.TrFacilities, nameof(TrFacilitie.Id), nameof(TrFacilitie.Title));
            ViewBag.TrFeatures = new SelectList(_context.TrFeatures, nameof(TrFeature.Id), nameof(TrFeature.Title));
            ViewBag.Difficulties = new SelectList(_context.Difficulties, nameof(Difficulty.Id), nameof(Difficulty.Name));
            ViewBag.Video = trekking.VideoUrl;

            return View(update);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateTrekkingVM update)
        {
            if (id is null || id == 0) return BadRequest();

            var video = update.Video;
            var result1 = video?.CheckValidate("video/", 2000);
            if (result1?.Length > 0)
            {
                ModelState.AddModelError("Video", result1);
            }

            foreach (int featureId in (update.TrFeaturesIds ?? new List<int>()))
            {
                if (!_context.TrFeatures.Any(c => c.Id == featureId))
                {
                    ModelState.AddModelError("TrFeaturesIds", "There is no matched feature with this id!");
                    break;
                }
            }
            foreach (int facilitieId in (update.TrFacilitiesIds ?? new List<int>()))
            {
                if (!_context.TrFacilities.Any(c => c.Id == facilitieId))
                {
                    ModelState.AddModelError("TrFacilitiesIds", "There is no matched facilitie with this id!");
                    break;
                }
            }
            foreach (int difficultyId in (update.DifficultiesIds ?? new List<int>()))
            {
                if (!_context.Difficulties.Any(c => c.Id == difficultyId))
                {
                    ModelState.AddModelError("DifficultiesIds", "There is no matched difficulty with this id!");
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
                ViewBag.TrFacilities = new SelectList(_context.TrFacilities, nameof(TrFacilitie.Id), nameof(TrFacilitie.Title));
                ViewBag.TrFeatures = new SelectList(_context.TrFeatures, nameof(TrFeature.Id), nameof(TrFeature.Title));
                ViewBag.Difficulties = new SelectList(_context.Difficulties, nameof(Difficulty.Id), nameof(Difficulty.Name));
                ViewBag.Video = _context.Trekkings.FirstOrDefault(t => t.Id == id).VideoUrl;
                return View();
            }
            var trekking = _context.Trekkings.Include(t => t.TrekkingFeatures).Include(t => t.TrekkingFacilities).Include(t => t.TrekkingDifficulties)
                .Include(p => p.TrekkingImages).FirstOrDefault(p => p.Id == id);
            if (trekking is null) return NotFound();

            if (video != null)
            {
                string newVideo = video.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "trekking"));
                trekking.VideoUrl.DeleteFile(_env.WebRootPath, "assets/images/trekking");
                trekking.VideoUrl = newVideo;
            }

            foreach (var item in trekking.TrekkingFacilities)
            {
                if (update.TrFacilitiesIds.Contains(item.TrFacilitieId))
                {
                    update.TrFacilitiesIds.Remove(item.TrFacilitieId);
                }
                else
                {
                    _context.TrekkingFacilities.Remove(item);
                }
            }
            foreach (var facilitieId in update.TrFacilitiesIds)
            {
                _context.TrekkingFacilities.Add(new TrekkingFacilitie { Trekking = trekking, TrFacilitieId = facilitieId });
            }
            foreach (var item in trekking.TrekkingDifficulties)
            {
                if (update.DifficultiesIds.Contains(item.DifficultyId))
                {
                    update.DifficultiesIds.Remove(item.DifficultyId);
                }
                else
                {
                    _context.TrekkingDifficulties.Remove(item);
                }
            }
            foreach (var difficultyId in update.DifficultiesIds)
            {
                _context.TrekkingDifficulties.Add(new TrekkingDifficulty { Trekking = trekking, DifficultyId = difficultyId });
            }

            foreach (var item in trekking.TrekkingFeatures)
            {
                if (update.TrFeaturesIds.Contains(item.TrFeatureId))
                {
                    update.TrFeaturesIds.Remove(item.TrFeatureId);
                }
                else
                {
                    _context.TrekkingFeatures.Remove(item);
                }
            }
            foreach (var featureId in update.TrFeaturesIds)
            {
                _context.TrekkingFeatures.Add(new TrekkingFeature { Trekking = trekking, TrFeatureId = featureId });
            }

            List<TrekkingImage> images = new List<TrekkingImage>();

            if (primaryImg != null)
            {
                var oldCover = trekking.TrekkingImages.FirstOrDefault(ti => ti.IsPrimary == true);
                _context.TrekkingImages.Remove(oldCover);
                oldCover.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/trekking");
                images.Add(new TrekkingImage
                {
                    ImageUrl = primaryImg.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "trekking")),
                    IsPrimary = true,
                    Trekking=trekking
                });
            }


            foreach (var item in otherImgs)
            {
                images.Add(new TrekkingImage
                {
                    ImageUrl = item?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "trekking")),
                    IsPrimary = null,
                    Trekking = trekking
                });
            }
            var delete = update.ImageIds;
            foreach (var item in (delete ?? new List<int>()))
            {
                foreach (var pi in trekking.TrekkingImages)
                {
                    if (pi.Id == item)
                    {
                        trekking.TrekkingImages.Remove(pi);
                        pi.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/trekking");
                    }
                }
            }
            foreach (var image in images)
            {
                trekking.TrekkingImages.Add(image);

            }
            trekking.Title = update.Title;
            trekking.Description = update.Description;
            trekking.Name = update.Name;
            trekking.Price = update.Price;
            trekking.GroupSize = update.GroupSize;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
