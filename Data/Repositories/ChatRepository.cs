using Core.Models;
using Core.Repositories;
using Core.Services.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Data.Repositories
{
    public class ChatRepository : Repository<Chat>, IChatRepository
    {
        private readonly string TelegramChatKeyPrefix = "telegramchat";
        public ChatRepository(DbContext context, IRedisCacheService cache) : base(context, cache)
        { }

        public async Task<Chat> AddAccountToChatByChatId(int chatId, Account account)
        {
            var chat = await GetByIdWithAccountsAsync(chatId);
            if (chat.Accounts is not null)
            {
                chat.Accounts.Add(account);
            }
            else
            {
                chat.Accounts = [account];
            }
            return chat;
        }

        public async Task<Chat> GetChatByTelegramChatIdAsync(long telegramChatId)
        {
            string key = $"{TelegramChatKeyPrefix}:{telegramChatId}";
            async Task<Chat> getActiveChat() => await Context.Set<Chat>().SingleOrDefaultAsync(c => c.TelegramChatId == telegramChatId);
            var cacheOptions = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) };

            return await Cache.GetOrSetAsync(key, getActiveChat, cacheOptions);
        }

        public async Task<Chat> GetByIdWithAccountsAsync(int chatId)
        {
            return await Context.Set<Chat>().Include(c => c.Accounts).SingleOrDefaultAsync();
        }

        public override async void DeleteAsync(Chat entity, long deletedById, string deletedByName)
        {
            base.DeleteAsync(entity, deletedById, deletedByName);

            string key = $"{TelegramChatKeyPrefix}:{entity.TelegramChatId}";
            await Cache.RemoveAsync(key);
        }
    }
}
