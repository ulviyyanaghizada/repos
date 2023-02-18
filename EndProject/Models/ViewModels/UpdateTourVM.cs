using EndProject.Models.AllTourInfo;

namespace EndProject.Models.ViewModels
{
    public class UpdateTourVM
    {
        public ICollection<IFormFile>? OtherImages { get; set; }
        public IFormFile? PrimaryImage { get; set; }
        public ICollection<TourImage>? TourImages { get; set; }
        public List<int>? ImageIds { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public byte GroupSize { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public List<int> CategoriesIds { get; set; }
        public List<int> TFacilitiesIds { get; set; }
        public List<int> TFeaturesIds { get; set; }
        public IFormFile? Video { get; set; }
        public string? VideoUrl { get; set; }
    }
}
