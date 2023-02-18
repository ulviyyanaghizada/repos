using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TourCategory:BaseEntity
    {
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
