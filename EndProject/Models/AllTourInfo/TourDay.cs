using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TourDay:BaseEntity
    {
        public int TourId { get; set; }
        public  Tour? Tour { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<TourDaysImage>? TourDaysImages { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }

    }
}
