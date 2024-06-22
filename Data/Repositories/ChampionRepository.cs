using Core.Models;
using Core.Repositories;
using Core.Services.Cache;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ChampionRepository(DbContext context, IRedisCacheService cache)
        : Repository<Champion>(context, cache), IChampionRepository
    {
        public async Task<Champion> GetByChampionId(int championId)
        {
            return await Context.Set<Champion>().FirstOrDefaultAsync(c => c.ChampionId == championId);
        }
    }
}
