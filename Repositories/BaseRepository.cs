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
            await this.context.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(List<TEntity> entities)
        {
            await this.context.AddRangeAsync(entities);
            await this.context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await this.context.Set<TEntity>().ToListAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            this.context.Update(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(List<TEntity> entities)
        {
            context.UpdateRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            this.context.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(List<TEntity> entities)
        {
            context.RemoveRange(entities);
            await context.SaveChangesAsync();
        }
    }
}
