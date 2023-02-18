using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Hotel:BaseNameEntity
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
