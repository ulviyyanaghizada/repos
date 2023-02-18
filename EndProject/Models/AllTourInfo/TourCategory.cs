using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TourCategory:BaseEntity
    {
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
        public int TCategoryId { get; set; }
        public TCategory? TCategory { get; set; }
    }
}
