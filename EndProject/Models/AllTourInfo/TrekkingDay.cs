using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TrekkingDay : BaseEntity
    {
        public int TrekkingId { get; set; }
        public Trekking? Trekking { get; set; }
        public string Description { get; set; }
    }
}
