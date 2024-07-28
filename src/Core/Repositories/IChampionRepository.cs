using Core.Models;

namespace Core.Repositories
{
    public interface IChampionRepository : IRepository<Champion>
    {
        public Task<Champion> GetByChampionId(int championId);
    }
}
