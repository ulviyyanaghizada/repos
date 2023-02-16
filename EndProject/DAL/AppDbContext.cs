using EndProject.Models;
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
    }
}
