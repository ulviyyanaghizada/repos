using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TrFacilitie : BaseEntity
    {
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public ICollection<TrekkingFacilitie>? TrekkingFacilities { get; set; }

    }
}
