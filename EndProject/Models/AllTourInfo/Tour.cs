using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Tour:BaseNameEntity
    {
        public string  Description { get; set; }
        public string  Title { get; set; }
        public string VideoUrl { get; set; }
        public double Price { get; set; }
        public byte GroupSize { get; set; }
        public ICollection<TourImage>? TourImages { get; set; }
        public ICollection<TourFacilitie>? TourFacilities { get; set; }
        public ICollection<TourFeature>? TourFeatures { get; set; }
        public ICollection<TourDay> TourDays { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public ICollection<TourCategory>? TourCategories { get; set; } 

    }
}
