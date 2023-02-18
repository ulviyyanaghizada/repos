namespace EndProject.Models.ViewModels
{
    public class UpdateTourCountryVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public int ContinentId { get; set; }
    }
}
