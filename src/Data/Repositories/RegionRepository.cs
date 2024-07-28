using Core.Models;
using Core.Repositories;
using Core.Services.Cache;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class RegionRepository(DbContext context, IRedisCacheService cache)
        : Repository<Region>(context, cache), IRegionRepository
    {
    }
}
