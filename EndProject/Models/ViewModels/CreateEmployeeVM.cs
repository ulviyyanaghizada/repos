namespace EndProject.Models.ViewModels
{
    public class CreateEmployeeVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string About { get; set; }
        public string Description { get; set; }
        public string Mail { get; set; }

        public string FacebookLink { get; set; }
        public string YoutubeLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public IFormFile Image { get; set; }
        public int PositionId { get; set; }

    }
}
