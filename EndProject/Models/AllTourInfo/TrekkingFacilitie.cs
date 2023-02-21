using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TrekkingFacilitie:BaseEntity
    {
        public int TrekkingId { get; set; }
        public Trekking? Trekking { get; set; }
        public int TrFacilitieId { get; set; }
        public TrFacilitie? TrFacilitie { get; set; }
    }
}
