namespace PhysicsWars.Application.Common.DataAccess;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}