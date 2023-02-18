using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TourTrekking:BaseEntity
    {
        public int TrekkingId { get; set; }
        public Trekking Trekking { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
}
