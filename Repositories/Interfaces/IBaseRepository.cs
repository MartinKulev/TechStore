namespace TechStore.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task CreateAsync(TEntity entity);

        Task CreateRangeAsync(List<TEntity> entities);

        Task<List<TEntity>> GetAllAsync();

        Task UpdateAsync(TEntity entity);

        Task UpdateRangeAsync(List<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        Task DeleteRangeAsync(List<TEntity> entities);
    }
}
