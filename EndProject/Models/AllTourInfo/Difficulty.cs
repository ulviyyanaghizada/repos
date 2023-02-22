using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Difficulty:BaseNameEntity
    {
		public ICollection<Trekking>? Trekkings { get; set; }

	}
}
