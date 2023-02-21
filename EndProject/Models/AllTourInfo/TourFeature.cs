using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TourFeature:BaseEntity
    {
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
        public int TFeatureId { get; set; }
        public TFeature? TFeature { get; set;}
    }
}
