namespace EndProject.Models.ViewModels
{
    public class CreateTourDayVM
    {
        public ICollection<IFormFile>? OtherImages { get; set; }
        public IFormFile PrimaryImage { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int HotelId { get; set; }
        public int TourId { get; set; }

    }
}
