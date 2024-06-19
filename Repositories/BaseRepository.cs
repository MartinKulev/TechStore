using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechStore.Data;
using TechStore.Data.Entities;
using TechStore.Repositories.Interfaces;

namespace TechStore.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly TechStoreDbContext context;

        public BaseRepository(TechStoreDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(List<TEntity> entities)
        {
            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(List<TEntity> entities)
        {
            context.UpdateRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            context.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(List<TEntity> entities)
        {
            context.RemoveRange(entities);
            await context.SaveChangesAsync();
        }
    }
}
