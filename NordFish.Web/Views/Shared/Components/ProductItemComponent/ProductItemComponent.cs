using Microsoft.AspNetCore.Mvc;
using NordFish.Database.Entities;
using NordFish.Services.UserServices;

namespace NordFish.Web.Views.Shared.Components.ProductItemComponent
{
    public class ProductItemComponent : ViewComponent
    {
        private readonly ICurrentUser _currentUser;

        public ProductItemComponent(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task<IViewComponentResult> InvokeAsync(ProductEntity productEntity)
        {
            ProductItemViewModel vm = new ProductItemViewModel()
            {
                BuyerId = _currentUser.Id,
                Product = productEntity,
                IsAuthorize = _currentUser.IsAuthenticated,
            };
            return View(vm);
        }
    }
}
