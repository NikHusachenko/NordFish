using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NordFish.Database.Entities;
using NordFish.Services.UserServices;
using NordFish.Services.UserServices.Models;
using NordFish.Web.Models.User;
using NordFishServices.ProductServices;
using NordFishServices.UserServices;
using NordFishServices.UserServices.Models;

namespace NordFish.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;
        private readonly IProductServices _productServices;

        public UserController(IUserService userService,
            ICurrentUser currentUser,
            IProductServices productServices)
        {
            _userService = userService;
            _currentUser = currentUser;
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]SignInViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { responseMessage = "Invalid data" });
            }

            if(!await _userService.SignIn(vm))
            {
                return BadRequest(new { responseMessage = "Not found" });
            }

            return Ok(new { responseMessage = Url.Action("Index", "Home") });
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody]SignUpViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { responseMessage = "Invalid data" });
            }

            if(!await _userService.SignUp(vm))
            {
                return BadRequest(new { responseMessage = "Was created" });
            }

            return Ok(new { responseMessage = Url.Action("Index", "Home") });
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            _userService.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            UserEntity userEntity = await _userService.GetByIdAsync(long.Parse(_currentUser.Id.ToString()));
            CartViewModel vm = new CartViewModel() { Products = userEntity.Products };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> BuyAll()
        {
            UserEntity userEntity = await _userService.GetByIdAsync(long.Parse(_currentUser.Id.ToString()));

            for (int i = userEntity.Products.Count - 1; i >= 0; i--)
            {
                await _productServices.Delete(userEntity.Products[i].Id);
            }
            return RedirectToAction("Cart", "User");
        }
    }
}
