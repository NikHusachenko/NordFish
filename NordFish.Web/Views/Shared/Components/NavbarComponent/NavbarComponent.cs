using Microsoft.AspNetCore.Mvc;
using NordFish.Database.Entities;
using NordFish.Services.UserServices;
using NordFishServices.UserServices;

namespace NordFish.Web.Views.Shared.Components.NavbarComponent
{
    public class NavbarComponent : ViewComponent
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;

        public NavbarComponent(ICurrentUser currentUser,
            IUserService userService)
        {
            _currentUser = currentUser;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            NavbarViewModel vm = new NavbarViewModel()
            {
                IsAdmin = _currentUser.IsAdmin,
                IsAuthorize = _currentUser.IsAuthenticated,
            };

            string userIdString = _currentUser.Id.ToString();

            if (userIdString != String.Empty)
            {
                UserEntity user = await _userService.GetByIdAsync(long.Parse(userIdString));
                vm.CartLength = user.Products?.Count;
            }

            return View(vm);
        }
    }
}
