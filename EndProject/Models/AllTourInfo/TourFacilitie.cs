using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class TourFacilitie:BaseEntity
    {
        public int TourId { get; set; }
        public Tour? Tour { get; set; }
        public int TFacilitieId { get; set; }
        public TFacilitie? TFacilitie { get; set; }
     }
}
