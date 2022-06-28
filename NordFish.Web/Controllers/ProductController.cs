using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NordFish.Database.Entities;
using NordFish.Database.Enumes;
using NordFish.Services.ProductServices.Models;
using NordFish.Web.Models.Product;
using NordFishServices.ProductServices;

namespace NordFish.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { responseMessage = "Invalid data" });
            }

            _productServices.Create(vm);
            return Ok(new { responseMessage = Url.Action("Settings", "Product") });
        }

        [HttpGet]
        public async Task<IActionResult> Settings()
        {
            SettingsViewModel vm = new SettingsViewModel() { Products = await _productServices.GetAllAsync() };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            DetailsViewModel vm = new DetailsViewModel() { Product = await _productServices.GetByIdAsync(id) };
            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            await _productServices.Delete(id);
            return RedirectToAction("Settings", "Product");
        }

        [HttpGet]
        public async Task<IActionResult> Show(int typeId = -1)
        {
            ShowViewModel vm = new ShowViewModel();
            if(typeId == -1)
            {
                vm.Products = await _productServices.GetAllNotBuyedAsync();
            }
            else
            {
                vm.Products = await _productServices.GetByType((ProductTypes)typeId);
            }

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Buy(long productId, long buyerId)
        {
            await _productServices.Buy(productId, buyerId);
            return RedirectToAction("Cart", "User");
        }
        
        [HttpGet]
        public async Task<IActionResult> CancelBuying(long id)
        {
            await _productServices.CancelBuying(id);
            return RedirectToAction("Cart", "User");
        }
    }
}