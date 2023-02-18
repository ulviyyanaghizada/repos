using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Category:BaseNameEntity
    {
        public ICollection<TourCategory>? TourCategories { get; set; }
    }
}
