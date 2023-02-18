using EndProject.Models.AllTourInfo;

namespace EndProject.Models.ViewModels
{
    public class UpdateTourDayVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int HotelId { get; set; }
        public int TourId { get; set; }
        public ICollection<IFormFile>? OtherImages { get; set; }
        public IFormFile? PrimaryImage { get; set; }
        public ICollection<TourDaysImage>? TourDaysImages { get; set; }
        public List<int>? ImageIds { get; set; }
    }
}
