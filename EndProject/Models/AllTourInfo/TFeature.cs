using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TFeature:BaseEntity
    {
        public string Title { get; set; }
        public ICollection<TourFeature>? TourFeatures { get; set; }
    }
}
