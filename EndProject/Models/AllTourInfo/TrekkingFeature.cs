using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TrekkingFeature : BaseEntity
    {
        public int TrekkingId { get; set; }
        public Trekking? Trekking { get; set; }
        public int TrFeatureId { get; set; }
        public TrFeature? TrFeature { get; set; }
    }
}