namespace EndProject.Models.ViewModels
{
    public class CreateAgencyVM
    {
        public string Description { get; set; }
        public string FirstTitle { get; set; }
        public string SecondTitle { get; set; }
        public string ThirdTitle { get; set; }
        public string Video { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile ImageCover { get; set; }
    }
}
