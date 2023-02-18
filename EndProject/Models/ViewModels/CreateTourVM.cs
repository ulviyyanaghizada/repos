namespace EndProject.Models.ViewModels
{
    public class CreateTourVM
    {
        public ICollection<IFormFile>? OtherImages { get; set; }
        public IFormFile PrimaryImage { get; set; }
        public IFormFile Video { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public byte GroupSize { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public List<int> TCategoriesIds { get; set; }
        public List<int> TFacilitiesIds { get; set; }
        public List<int> TFeaturesIds { get; set; }


    }
}
