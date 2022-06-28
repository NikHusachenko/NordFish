using Microsoft.EntityFrameworkCore;

namespace NordFish.EntityFramework.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public DbSet<TEntity> Table { get; set; }

        void Create(TEntity item);
        Task<TEntity> FindByIdAsync(long id);
        Task<List<TEntity>> GetAsync();
        List<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
