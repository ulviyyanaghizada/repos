using EndProject.Models.Base;

namespace EndProject.Models.AllTourInfo
{
    public class Room:BaseNameEntity
    {
        public double Price { get; set; }
        public ICollection<HotelRoom>? HotelRooms { get; set; }
    }
}
