using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TFacilitie:BaseEntity
    {
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public ICollection<TourFacilitie>? TourFacilities { get; set; }
    }
}
