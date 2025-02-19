using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<bool> AlredyExistsAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> UpdateAsync(TEntity existingEntity);
    }
}