using EndProject.Models.Base;

namespace EndProject.Models
{
	public class Condition:BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
	}
}
