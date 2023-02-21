using EndProject.Models;
using EndProject.Models.AllTourInfo;
using Microsoft.EntityFrameworkCore;

namespace EndProject.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Choose> Chooses { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<MostFrequent> MostFrequents { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PFeature> PFeatures { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<OfficeMap> OfficeMaps { get; set; }
        public DbSet<ContinentInfo> ContinentInfos { get; set; }
        public DbSet<MostAnswer> MostAnswers { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<TCategory> TCategories { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<TFacilitie> TFacilities { get; set; }
        public DbSet<TFeature> TFeatures { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourCategory> TourCategories { get; set; }
        public DbSet<TourDay> TourDays { get; set; }
        public DbSet<TourDaysImage> TourDaysImages { get; set; }
        public DbSet<TourFacilitie> TourFacilities { get; set; }
        public DbSet<TourFeature> TourFeatures { get; set; }
        public DbSet<TourImage> TourImages { get; set; }

        public DbSet<Trekking> Trekkings { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<TrekkingDifficulty> TrekkingDifficulties { get; set; }
        public DbSet<TrekkingFacilitie> TrekkingFacilities { get; set; }
        public DbSet<TrekkingFeature> TrekkingFeatures { get; set; }
        public DbSet<TrekkingImage> TrekkingImages { get; set; }
        public DbSet<TrFacilitie> TrFacilities { get; set; }
        public DbSet<TrFeature> TrFeatures { get; set; }
        public DbSet<TrekkingDay> TrekkingDays { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }

        public DbSet<ContactUs> ContactUs { get; set; }

    }
}
