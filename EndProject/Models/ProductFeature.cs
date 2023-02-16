using EndProject.Models.Base;

namespace EndProject.Models
{
    public class ProductFeature:BaseEntity
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int PFeatureId { get; set; }
        public PFeature? PFeature { get; set; }
    }
}
