using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TFacilitie:BaseEntity
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public ICollection<TourFacilitie>? TourFacilities { get; set; }
    }
}
