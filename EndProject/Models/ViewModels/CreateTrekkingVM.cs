namespace EndProject.Models.ViewModels
{
    public class CreateTrekkingVM
    {
		public string Name { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public byte GroupSize { get; set; }
        public IFormFile Video { get; set; }
		public int DifficultyId { get; set; }
        public string Description { get; set; }
        public ICollection<IFormFile>? OtherImages { get; set; }
        public IFormFile PrimaryImage { get; set; }
        public List<int> TrFacilitiesIds { get; set; }
        public List<int> TrFeaturesIds { get; set; }

    }
}
