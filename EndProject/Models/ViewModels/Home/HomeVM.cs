namespace EndProject.Models.ViewModels.Home
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

    }
}
