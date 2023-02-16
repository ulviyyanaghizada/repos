using EndProject.Models.Base;

namespace EndProject.Models
{
    public class Color:BaseNameEntity
    {
        public ICollection<ProductColor>? ProductColors { get; set; }
    }
}
