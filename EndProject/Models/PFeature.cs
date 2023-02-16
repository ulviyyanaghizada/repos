using EndProject.Models.Base;

namespace EndProject.Models
{
    public class PFeature:BaseEntity
    {
        public string Title { get; set; }
        public ICollection<ProductFeature>? ProductFeatures { get; set; }

    }
}
