using EndProject.DAL;
using EndProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Controllers.Shop
{
    public class ProductDetails : Controller
    {
        readonly AppDbContext _context;

        public ProductDetails(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult ProductDetail(int id)
        {

            HomeVM home = new HomeVM
            {
                
                  Products = _context.Products?.Include(p => p.ProductColors).ThenInclude(pc => pc.Color)
                .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).
                Include(p => p.ProductTags).ThenInclude(pt => pt.Tag).
                Include(p => p.ProductFeatures).ThenInclude(pf => pf.PFeature).Include(p => p.ProductImages),

                  Product = _context.Products.Include(p => p.ProductColors).ThenInclude(pc => pc.Color)
                .Include(p => p.ProductCategories).ThenInclude(pc => pc.Category).
                Include(p => p.ProductTags).ThenInclude(pt => pt.Tag).
                Include(p => p.ProductFeatures).ThenInclude(pf => pf.PFeature).Include(p => p.ProductImages).FirstOrDefault(p=>p.Id==id)
            };

            return View(home);
        }
    }
}
