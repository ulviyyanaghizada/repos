using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TourImage : BaseEntity
    {
        public string? ImageUrl { get; set; }
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
        public bool? IsPrimary { get; set; }
    }
}
