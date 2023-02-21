using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TrekkingImage:BaseEntity
    {
        public string? ImageUrl { get; set; }
        public int TrekkingId { get; set; }
        public Trekking? Trekking { get; set; }
        public bool? IsPrimary { get; set; }
    }
}
