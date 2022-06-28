using NordFish.Database.Entities;

namespace NordFish.Web.Models.Product
{
    public class SettingsViewModel
    {
        public List<ProductEntity> Products { get; set; }

        public SettingsViewModel()
        {
            Products = new List<ProductEntity>();
        }
    }
}
