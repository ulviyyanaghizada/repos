namespace EndProject.Models.ViewModels
{
    public class UpdateHotelVM
    {
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public string MealDescription { get; set; }
        public string? BreakFast { get; set; }
        public string? Lunch { get; set; }
        public string? Supper { get; set; }
        public List<int> RoomIds { get; set; }
    }
}
