namespace EndProject.Models.ViewModels
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public double SellPrice { get; set; }
        public double CostPrice { get; set; }
        public ICollection<IFormFile>? OtherImages { get; set; }
        public ICollection<ProductImage>? ProductImages { get; set; }
        public List<int>? ImageIds { get; set; }
        public List<int> PFeatureIds { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> TagIds { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
