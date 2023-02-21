using EndProject.Models.AllTourInfo;

namespace EndProject.Models.ViewModels
{
    public class DestinationVM
    {
        public ICollection<Continent> Continents { get; set; }
        public ICollection<Country> Countires { get; set; }

        
    }
}
