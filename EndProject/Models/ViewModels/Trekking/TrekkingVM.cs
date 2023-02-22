using EndProject.Models.AllTourInfo;

namespace EndProject.Models.ViewModels
{
	public class TrekkingVM
	{
		public ICollection<Difficulty> Difficulties { get; set; }
		public ICollection<Trekking> Trekkings { get; set; }

	}
}
