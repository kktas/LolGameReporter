using Core.Models.ModelBase;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class Repository<TEntity>(DbContext context, IDistributedCache distributedCache) : IRepository<TEntity> where TEntity : Model
    {
        protected readonly DbContext Context = context;

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

        public async ValueTask<TEntity> GetByIdAsync(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public void Delete(TEntity entity, long deletedById, string deletedByName)
        {
            entity.DeletedAt = DateTime.UtcNow;
            entity.DeletedById = deletedById;
            entity.DeletedByName = deletedByName;
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
