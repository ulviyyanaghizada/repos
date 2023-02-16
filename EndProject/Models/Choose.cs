using EndProject.Models.Base;

namespace EndProject.Models
{
    public class Choose:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
