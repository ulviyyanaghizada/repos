using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Trekking:BaseNameEntity
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public byte GroupSize { get; set; }
        public string VideoUrl { get; set; }
        public string Description { get; set; }
        public int DifficultyId { get; set; }
        public Difficulty? Difficulty { get; set; }
        public ICollection<TrekkingImage>? TrekkingImages { get; set; }
        public ICollection<TrekkingFacilitie>? TrekkingFacilities { get; set; }
        public ICollection<TrekkingFeature>? TrekkingFeatures { get; set; }
        public ICollection<TrekkingDay> TrekkingDays { get; set; }

    }
}
