using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Data.Repositories
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        public ChatRepository(DbContext context, IDistributedCache cache) : base(context, cache)
        { }

        public async Task<Chat> GetActiveChatByTelegramChatIdAsync(long telegramChatId)
        {
            return await SingleOrDefaultAsync(c => c.TelegramChatId == telegramChatId && c.DeletedAt == null);
        }
    }
}
