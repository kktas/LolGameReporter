using Core.Models.ModelBase;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : Model
    {
        public Task<TEntity> GetByIdAsync(int id);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void DeleteAsync(TEntity entity, long deletedBy, string deletedByName);
        void DeleteRange(IEnumerable<TEntity> entities);
    }
}
