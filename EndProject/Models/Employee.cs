using EndProject.Models.Base;
namespace EndProject.Models
{
    public class Employee:BaseNameEntity
    {
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string About { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Mail { get; set; }
        public string FacebookLink { get; set; }
        public string YoutubeLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public int PositionId { get; set; }
        public Position? Position { get; set; }

    }
}
