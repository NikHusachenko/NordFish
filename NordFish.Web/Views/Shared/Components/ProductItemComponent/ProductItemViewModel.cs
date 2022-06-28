using NordFish.Database.Entities;

namespace NordFish.Web.Views.Shared.Components.ProductItemComponent
{
    public class ProductItemViewModel
    {
        public ProductEntity Product { get; set; }
        public long? BuyerId { get; set; }
        public bool? IsAuthorize { get; set; }
    }
}
