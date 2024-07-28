using Core.Models;
using Core.Models.ModelBase;
using Core.Repositories;
using Core.Services.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class Repository<TEntity>(DbContext context, IRedisCacheService cache) : IRepository<TEntity> where TEntity : Model
    {
        protected readonly DbContext Context = context;
        protected readonly IRedisCacheService Cache = cache;
        protected readonly string EntityTypeName = typeof(TEntity).Name.ToLower();

        public async Task AddAsync(TEntity entity)
        {

            await Context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            string key = $"{EntityTypeName}:{id}";
            var cacheOptions = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) };

            Func<Task<TEntity>> function = async () => await Context.Set<TEntity>().FindAsync(id);

            return await Cache.GetOrSetAsync(key, function, cacheOptions);
        }

        public virtual void DeleteAsync(TEntity entity, long deletedById, string deletedByName)
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.DeletedById = deletedById;
            entity.DeletedByName = deletedByName;

            string key = $"{EntityTypeName}:{entity.Id}";
            Cache.RemoveAsync(key);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.DeletedAt = DateTime.UtcNow;
            }
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }
    }
}
