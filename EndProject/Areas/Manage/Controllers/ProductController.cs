using EndProject.DAL;
using EndProject.Models;
using EndProject.Models.ViewModels;
using EndProject.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Color = EndProject.Models.Color;

namespace EndProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        AppDbContext _context { get; }
        IWebHostEnvironment _env { get; }
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Products?.Include(p => p.ProductColors).ThenInclude(pc => pc.Color)
                .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).
                Include(p => p.ProductTags).ThenInclude(pt => pt.Tag).
                Include(p => p.ProductFeatures).ThenInclude(pf => pf.PFeature).Include(p => p.ProductImages));
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            Product exist = _context.Products.Include(p => p.ProductCategories)
                .Include(p => p.ProductTags).Include(p => p.ProductColors)
                .Include(p => p.ProductImages).Include(p=>p.ProductFeatures).FirstOrDefault(p => p.Id == id);
            if (exist is null) return NotFound();
            foreach (ProductImage image in exist.ProductImages)
            {
                image.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/product");
            }
            _context.ProductColors.RemoveRange(exist.ProductColors);
            _context.ProductTags.RemoveRange(exist.ProductTags);
            _context.ProductCategories.RemoveRange(exist.ProductCategories);
            _context.ProductFeatures.RemoveRange(exist.ProductFeatures);
            _context.ProductImages.RemoveRange(exist.ProductImages);
			_context.Products.Remove(exist);
			_context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            ViewBag.Colors = new SelectList(_context.Colors, nameof(Color.Id), nameof(Color.Name));
            ViewBag.Tags = new SelectList(_context.Tags, nameof(Tag.Id), nameof(Tag.Name));
            ViewBag.Categories = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));
            ViewBag.PFeatures = new SelectList(_context.PFeatures, nameof(PFeature.Id), nameof(PFeature.Title));
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateProductVM cp)
        {
           
            var otherImgs = cp.OtherImages ?? new List<IFormFile>();
            var primaryimg = cp.PrimaryImage;
            string result = primaryimg.CheckValidate("image/", 600);
            if (result.Length >0)
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
            foreach (int colorId in (cp.ColorIds ?? new List<int>()))
            {
                if (!_context.Colors.Any(c => c.Id == colorId))
                {
                    ModelState.AddModelError("ColorIds", "There is no matched color with this id!");
                    break;
                }
            }
            foreach (int tagId in (cp.TagIds ?? new List<int>()))
            {
                if (!_context.Tags.Any(c => c.Id == tagId))
                {
                    ModelState.AddModelError("TagIds", "There is no matched tag with this id!");
                    break;
                }
            }
            foreach (int categoryId in (cp.CategoryIds ?? new List<int>()))
            {
                if (!_context.Categories.Any(c => c.Id == categoryId))
                {
                    ModelState.AddModelError("CategoryIds", "There is no matched category with this id!");
                    break;
                }
            }foreach (int pFeatureId in (cp.PFeatureIds ?? new List<int>()))
            {
                if (!_context.PFeatures.Any(c => c.Id == pFeatureId))
                {
                    ModelState.AddModelError("PFeatureIds", "There is no matched feature with this id!");
                    break;
                }
            }
            if (!ModelState.IsValid)
            {

                ViewBag.Colors = new SelectList(_context.Colors, nameof(Color.Id), nameof(Color.Name));
                ViewBag.Tags = new SelectList(_context.Tags, nameof(Tag.Id), nameof(Tag.Name));
                ViewBag.Categories = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));
                ViewBag.PFeatures = new SelectList(_context.PFeatures, nameof(PFeature.Id), nameof(PFeature.Title));

                return View();
            }
            var tags = _context.Tags.Where(t => cp.TagIds.Contains(t.Id));
            var colors = _context.Colors.Where(c => cp.ColorIds.Contains(c.Id));
            var categories = _context.Categories.Where(ca => cp.CategoryIds.Contains(ca.Id));
            var pFeatures = _context.PFeatures.Where(f => cp.PFeatureIds.Contains(f.Id));
            Product product = new Product
            {
                Title = cp.Title,
                CostPrice = cp.CostPrice,
                SellPrice = cp.SellPrice,
                Description = cp.Description,
                Count=cp.Count

            };
            List<ProductImage> images = new List<ProductImage>();
            images.Add(new ProductImage
            {
                ImageUrl = primaryimg?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "product")),
                Product = product,
                IsPrimary = true
            });
            foreach (var item in otherImgs)
            {
                images.Add(
                    new ProductImage
                    {
                        ImageUrl = item?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "product")),
                        Product = product,
                        IsPrimary = false
                    });
            }
            product.ProductImages = images;
            _context.Products.Add(product);
            foreach (var item in colors)
            {
                _context.ProductColors.Add(new ProductColor { Product = product, ColorId = item.Id });
            }
            foreach (var item in tags)
            {
                _context.ProductTags.Add(new ProductTag { Product = product, TagId = item.Id });
            }
            foreach (var item in categories)
            {
                _context.ProductCategories.Add(new ProductCategory { Product = product, CategoryId = item.Id });
            } 
            foreach (var item in pFeatures)
            {
                _context.ProductFeatures.Add(new ProductFeature { Product = product, PFeatureId = item.Id });
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            if (id is null || id == 0) return BadRequest();
            var product = _context.Products.Include(p => p.ProductCategories).Include(p => p.ProductColors).Include(p => p.ProductTags)
                .Include(p => p.ProductImages).Include(p => p.ProductFeatures).FirstOrDefault(p => p.Id == id);
            if (product is null) return NotFound();
            UpdateProductVM updateProduct = new UpdateProductVM
            {
                Id = product.Id,
                Title = product.Title,
                CostPrice = product.CostPrice,
                SellPrice = product.SellPrice,
                Description = product.Description,
                Count = product.Count,
                ProductImages = product.ProductImages.ToList(),
                PFeatureIds = product.ProductFeatures.Select(pc=>pc.PFeatureId).ToList(),
                ColorIds = product.ProductColors.Select(pc => pc.ColorId).ToList(),
                TagIds = product.ProductTags.Select(pc => pc.TagId).ToList(),
                CategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList()
            };
            ViewBag.Colors = new SelectList(_context.Colors, nameof(Color.Id), nameof(Color.Name));
            ViewBag.Tags = new SelectList(_context.Tags, nameof(Tag.Id), nameof(Tag.Name));
            ViewBag.Categories = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));
            ViewBag.PFeatures = new SelectList(_context.PFeatures, nameof(PFeature.Id), nameof(PFeature.Title));

            return View(updateProduct);
        }
        [HttpPost]
        public IActionResult Update(int? id, UpdateProductVM updateProduct)
        {
            foreach (int colorId in (updateProduct.ColorIds ?? new List<int>()))
            {
                if (!_context.Colors.Any(c => c.Id == colorId))
                {
                    ModelState.AddModelError("ColorIds", "There is no matched color with this id!");
                    break;
                }
            }
            foreach (int tagId in (updateProduct.TagIds ?? new List<int>()))
            {
                if (!_context.Tags.Any(c => c.Id == tagId))
                {
                    ModelState.AddModelError("ColorIds", "There is no matched tag with this id!");
                    break;
                }
            }
            foreach (int categoryId in (updateProduct.CategoryIds ?? new List<int>()))
            {
                if (!_context.Categories.Any(c => c.Id == categoryId))
                {
                    ModelState.AddModelError("CategoryIds", "There is no matched category with this id!");
                    break;
                }
            }foreach (int PFeatureId in (updateProduct.PFeatureIds ?? new List<int>()))
            {
                if (!_context.PFeatures.Any(c => c.Id == PFeatureId))
                {
                    ModelState.AddModelError("PFeatureIds", "There is no matched feature with this id!");
                    break;
                }
            }
			var primaryImg = updateProduct.PrimaryImage;
			var otherImgs = updateProduct.OtherImages ?? new List<IFormFile>();
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
                ViewBag.Colors = new SelectList(_context.Colors, nameof(Color.Id), nameof(Color.Name));
                ViewBag.Tags = new SelectList(_context.Tags, nameof(Tag.Id), nameof(Tag.Name));
                ViewBag.Categories = new SelectList(_context.Categories, nameof(Category.Id), nameof(Category.Name));
                ViewBag.PFeatures = new SelectList(_context.PFeatures, nameof(PFeature.Id), nameof(PFeature.Title));

                return View();
            }
            var product = _context.Products.Include(p => p.ProductCategories).Include(p => p.ProductColors).Include(p => p.ProductTags)
                .Include(p => p.ProductImages).Include(p => p.ProductFeatures).FirstOrDefault(p => p.Id == id);
            if (product is null) return NotFound();
            foreach (var item in product.ProductColors)
            {
                if (updateProduct.ColorIds.Contains(item.ColorId))
                {
                    updateProduct.ColorIds.Remove(item.ColorId);
                }
                else
                {
                    _context.ProductColors.Remove(item);
                }
            }
            foreach (var colorId in updateProduct.ColorIds)
            {
                _context.ProductColors.Add(new ProductColor { Product = product, ColorId = colorId });
            }
            foreach (var item in product.ProductTags)
            {
                if (updateProduct.TagIds.Contains(item.TagId))
                {
                    updateProduct.TagIds.Remove(item.TagId);
                }
                else
                {
                    _context.ProductTags.Remove(item);
                }
            }
            foreach (var tagId in updateProduct.TagIds)
            {
                _context.ProductTags.Add(new ProductTag { Product = product, TagId = tagId });
            }
            foreach (var item in product.ProductCategories)
            {
                if (updateProduct.CategoryIds.Contains(item.CategoryId))
                {
                    updateProduct.CategoryIds.Remove(item.CategoryId);
                }
                else
                {
                    _context.ProductCategories.Remove(item);
                }
            }
            foreach (var categoryId in updateProduct.CategoryIds)
            {
                _context.ProductCategories.Add(new ProductCategory { Product = product, CategoryId = categoryId });
            }
            foreach (var item in product.ProductFeatures)
            {
                if (updateProduct.PFeatureIds.Contains(item.PFeatureId))
                {
                    updateProduct.PFeatureIds.Remove(item.PFeatureId);
                }
                else
                {
                    _context.ProductFeatures.Remove(item);
                }
            }
            foreach (var pFeatureId in updateProduct.PFeatureIds)
            {
                _context.ProductFeatures.Add(new ProductFeature { Product = product, PFeatureId = pFeatureId });
            }


            List<ProductImage> images = new List<ProductImage>();

			if (primaryImg != null)
			{
				var oldCover = product.ProductImages.FirstOrDefault(pi => pi.IsPrimary == true);
				_context.ProductImages.Remove(oldCover);
				oldCover.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/product");
				images.Add(new ProductImage
				{
					ImageUrl = primaryImg.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "product")),
					IsPrimary = true,
					Product = product
				});
			}


			foreach (var item in otherImgs)
            {
                images.Add(new ProductImage
                {
                    ImageUrl = item?.SaveFile(Path.Combine(_env.WebRootPath, "assets", "images", "product")),
					IsPrimary = null,
					Product = product
                });
            }
            var delete = updateProduct.ImageIds;
            foreach (var item in (delete ?? new List<int>()))
            {
                foreach (var pi in product.ProductImages)
                {
                    if (pi.Id == item)
                    {
                        product.ProductImages.Remove(pi);
                        pi.ImageUrl.DeleteFile(_env.WebRootPath, "assets/images/product");
                    }
                }
            }
            foreach (var image in images)
            {
                product.ProductImages.Add(image);

            }
            product.Title = updateProduct.Title;
            product.Description = updateProduct.Description;
            product.Count = updateProduct.Count;
            product.CostPrice = updateProduct.CostPrice;
            product.SellPrice = updateProduct.SellPrice;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
