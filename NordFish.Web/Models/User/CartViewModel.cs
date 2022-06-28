using NordFish.Database.Entities;

namespace NordFish.Web.Models.User
{
    public class CartViewModel
    {
        public List<ProductEntity> Products { get; set; }

        public CartViewModel()
        {
            Products = new List<ProductEntity>();
        }
    }
}
