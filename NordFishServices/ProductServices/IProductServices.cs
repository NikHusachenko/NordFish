using NordFish.Database.Entities;
using NordFish.Database.Enumes;
using NordFish.Services.ProductServices.Models;

namespace NordFishServices.ProductServices
{
    public interface IProductServices
    {
        void Create(CreateViewModel vm);
        void Update(ProductEntity productEntity);
        Task<bool> Delete(long id);

        Task<ProductEntity> GetByIdAsync(long Id);

        Task<List<ProductEntity>> GetAllAsync();
        Task<List<ProductEntity>> GetAllNotBuyedAsync();

        Task<List<ProductEntity>> GetByType(ProductTypes type);
        Task<List<ProductEntity>> GetByTypeNotBuyed(ProductTypes type);

        Task<bool> Buy(long productId, long buyerId);
        Task<bool> CancelBuying(long productId);
    }
}
