using EndProject.Models.Base;

namespace EndProject.Models
{
    public class Entry:BaseNameEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
