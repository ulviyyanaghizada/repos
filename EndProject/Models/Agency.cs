using EndProject.Models.Base;

namespace EndProject.Models
{
    public class Agency:BaseEntity
    {
        public string Description { get; set; }
        public string FirstTitle { get; set; }
        public string SecondTitle { get; set; }
        public string ThirdTitle { get; set; }
        public string Video { get; set; }
        public string ImageUrl { get; set; }
        public string ImageCoverUrl { get; set; }

    }
}
