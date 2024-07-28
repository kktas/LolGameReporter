using Core.Models;
using Core.Repositories;
using Core.Services.Cache;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data.Repositories
{
    public class AccountRepository(DbContext context, IRedisCacheService cache)
        : Repository<Account>(context, cache), IAccountRepository
    {
        public async Task<Account?> GetAccountByNameTagLinePuuidServerWithChats(string gameName, string tagLine, string puuid, int serverId)
        {
            return await Context.Set<Account>()
                .Include(a => a.Chats)
                .FirstOrDefaultAsync(a =>
                    a.GameName == gameName &&
                    a.TagLine == tagLine &&
                    a.Puuid == puuid &&
                    a.ServerId == serverId
                );
        }

        public async Task<List<Account>> GetAllAccountsWithServerWithChatsAsync()
        {
            return await Context.Set<Account>()
                .Include(a => a.Server)
                .Include(a => a.Chats)
                .ToListAsync();
        }
    }
}
