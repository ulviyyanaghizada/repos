using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TCategory:BaseNameEntity
    {
        public ICollection<TourCategory>? TourCategories { get; set; }
    }
}
