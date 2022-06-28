using NordFish.Database.Entities;

namespace NordFish.Web.Models.Product
{
    public class ShowViewModel
    {
        public List<ProductEntity> Products { get; set; }

        public ShowViewModel()
        {
            Products = new List<ProductEntity>();
        }
    }
}
