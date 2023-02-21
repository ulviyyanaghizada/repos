using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TrFeature : BaseEntity
    {
        public string Title { get; set; }
        public ICollection<TrekkingFeature>? TrekkingFeatures { get; set; }
    }
}
