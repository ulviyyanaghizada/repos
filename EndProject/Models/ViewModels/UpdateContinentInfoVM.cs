namespace EndProject.Models.ViewModels
{
    public class UpdateContinentInfoVM
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
