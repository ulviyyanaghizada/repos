using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Continent:BaseNameEntity
    {
        public ICollection<Country> Countries { get; set; }
    }
}
