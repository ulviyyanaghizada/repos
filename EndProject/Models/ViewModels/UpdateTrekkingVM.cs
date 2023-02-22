using EndProject.Models.AllTourInfo;

namespace EndProject.Models.ViewModels
{
    public class UpdateTrekkingVM
    {
		public int DifficultyId { get; set; }
		public List<int>? ImageIds { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public byte GroupSize { get; set; }
        public string Name { get; set; }
        public List<int> TrFacilitiesIds { get; set; }
        public List<int> TrFeaturesIds { get; set; }
        public IFormFile? Video { get; set; }
        public string? VideoUrl { get; set; }
        public ICollection<TrekkingImage>? TrekkingImages { get; set; }
        public ICollection<IFormFile>? OtherImages { get; set; }
        public IFormFile? PrimaryImage { get; set; }
    }
}
