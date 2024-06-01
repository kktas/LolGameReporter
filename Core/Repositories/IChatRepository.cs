using Core.Models;

namespace Core.Repositories
{
    public interface IChatRepository : IRepository<Chat>
    {
        public Task<Chat> GetActiveChatByTelegramChatIdAsync(long telegramChatId);
    }
}
