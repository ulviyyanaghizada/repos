using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TrekkingDifficulty : BaseEntity
    {
        public int TrekkingId { get; set; }
        public Trekking? Trekking { get; set; }
        public int DifficultyId { get; set; }
        public Difficulty? Difficulty { get; set; }
    }
}
