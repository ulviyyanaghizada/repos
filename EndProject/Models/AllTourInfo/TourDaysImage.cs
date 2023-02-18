using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TourDaysImage : BaseEntity
    {
        public string? ImageUrl { get; set; }
        public int TourDayId { get; set; }
        public TourDay? TourDay { get; set; }
        public bool? IsPrimary { get; set; }
    }
}
