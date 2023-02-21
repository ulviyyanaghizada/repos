using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Difficulty:BaseNameEntity
    {
        public ICollection<TrekkingDifficulty>? TrekkingDifficulties { get; set; }
    }
}
