using EndProject.Models.Base;

namespace EndProject.Models
{
    public class Category:BaseNameEntity
    {
        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}
