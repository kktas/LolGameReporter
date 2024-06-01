using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Data.Repositories
{
    public class AccountRepository(DbContext context, IDistributedCache distributedCache)
        : Repository<Account>(context, distributedCache), IAccountRepository
    {
    }
}
