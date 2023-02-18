using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Country:BaseNameEntity
    {
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Tour> Tours { get; set; }
        public int ContinentId { get; set; }
        public Continent? Continent { get; set; }
    }
}
