using Microsoft.EntityFrameworkCore;
using NordFish.Database.Entities;
using NordFish.Database.Enumes;
using NordFish.EntityFramework.Repository;
using NordFish.Services.ProductServices.Models;

namespace NordFishServices.ProductServices
{
    public class ProductServices : IProductServices
    {
        private readonly IGenericRepository<ProductEntity> _genericRepository;

        public ProductServices(IGenericRepository<ProductEntity> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<bool> Buy(long productId, long buyerId)
        {
            ProductEntity productEntity = await _genericRepository.Table
                .FirstOrDefaultAsync(product => product.Id == productId && product.UserFK == null);

            if(productEntity == null)
            {
                return false;
            }

            productEntity.UserFK = buyerId;
            Update(productEntity);
            return true;
        }

        public async Task<bool> CancelBuying(long productId)
        {
            ProductEntity product = await _genericRepository.Table
                .FirstOrDefaultAsync(product => product.Id == productId && product.UserFK == null);

            if(product == null)
            {
                return false;
            }

            product.UserFK = null;
            Update(product);
            return true;
        }

        public void Create(CreateViewModel vm)
        {
            ProductEntity productEntity = new ProductEntity()
            {
                CreatedOn = DateTime.Now,
                Description = vm.Description,
                Name = vm.Name,
                Price = vm.Price,
                Type = (ProductTypes)vm.Type,
                Weight = vm.Weight,
            };

            _genericRepository.Create(productEntity);
        }

        public async Task<bool> Delete(long id)
        {
            ProductEntity productEntity = await _genericRepository.Table
                .FirstOrDefaultAsync(product => product.Id == id);

            if(productEntity == null)
            {
                return false;
            }

            _genericRepository.Remove(productEntity);
            return true;
        }

        public async Task<List<ProductEntity>> GetAllAsync()
        {
            return await _genericRepository.Table.ToListAsync();
        }

        public async Task<List<ProductEntity>> GetAllNotBuyedAsync()
        {
            return await _genericRepository.Table
                .Where(product => product.UserFK == null)
                .ToListAsync();
        }

        public async Task<ProductEntity> GetByIdAsync(long id)
        {
            return await _genericRepository.Table
                .FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<List<ProductEntity>> GetByType(ProductTypes type)
        {
            return await _genericRepository.Table
                .Where(product => product.Type == type)
                .ToListAsync();
        }

        public async Task<List<ProductEntity>> GetByTypeNotBuyed(ProductTypes type)
        {
            return await _genericRepository.Table
                .Where(product => product.Type == type && product.UserFK == null)
                .ToListAsync();
        }

        public void Update(ProductEntity productEntity)
        {
            _genericRepository.Update(productEntity);
        }
    }
}