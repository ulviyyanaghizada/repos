using EndProject.Models.AllTourInfo;

namespace EndProject.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Entry> Entries { get; set; }
        public List<Choose> Chooses { get; set; }
        public IEnumerable<Agency> Agencies { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public Employee Employee { get; set; }
        public IEnumerable<OfficeMap> OfficeMaps { get; set; }
        public IEnumerable<ContinentInfo> ContinentInfos { get; set; }
        public List<MostFrequent> MostFrequents { get; set; }
        public List<MostAnswer> MostAnswers { get; set; }
        public List<Condition> Conditions { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public List<QuestionCategory> QuestionCategories { get; set; }
        public List<Question> Questions { get; set; }
        public Product Product { get; set; }
        public ICollection<Continent> Continents { get; set; }
        public ICollection<Country> Countires { get; set; }

    }
}
