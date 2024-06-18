using Core.Models;
using Core.Repositories;
using Core.Services.Cache;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ServerRepository(DbContext context, IRedisCacheService cache)
        : Repository<Server>(context, cache), IServerRepository
    {
    }
}
