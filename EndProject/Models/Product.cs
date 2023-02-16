using EndProject.Models.Base;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;

namespace EndProject.Models
{
    public class Product:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public double SellPrice { get; set; }
        public double CostPrice { get; set; }
        public ICollection<ProductColor>? ProductColors { get; set; }
        public ICollection<ProductTag>? ProductTags { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public ICollection<ProductFeature>? ProductFeatures { get; set; }
        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}
