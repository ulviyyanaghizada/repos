using EndProject.Models.Base;

namespace EndProject.Models
{
    public class Tag:BaseNameEntity
    {
        public ICollection<ProductTag>? ProductTags { get; set; }
    }
}
