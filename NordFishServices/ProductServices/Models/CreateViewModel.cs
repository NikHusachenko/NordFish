namespace NordFish.Services.ProductServices.Models
{
    public class CreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Weight { get; set; }
        public int Type { get; set; }
    }
}