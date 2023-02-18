using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class HotelRoom:BaseEntity
    {
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
