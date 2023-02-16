namespace EndProject.Models.ViewModels
{
    public class UpdateChooseVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}
