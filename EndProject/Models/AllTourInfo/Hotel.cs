using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Hotel:BaseNameEntity
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string MealDescription { get; set; }
        public string? BreakFast { get; set; }
        public string? Lunch { get; set; }
        public string? Supper { get; set; }
        public ICollection<TourDay>? TourDays { get; set; }
        public ICollection<HotelRoom>? HotelRooms { get; set; }
    }
}
