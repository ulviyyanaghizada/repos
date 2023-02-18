namespace EndProject.Models.ViewModels
{
    public class CreateTourCountryVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int ContinentId { get; set; }
    }
}
