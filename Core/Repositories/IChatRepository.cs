using Core.Models;

namespace Core.Repositories
{
    public interface IChatRepository : IRepository<Chat>
    {
        public Task<Chat> GetChatByTelegramChatIdAsync(long telegramChatId);
        public Task<Chat> GetByIdWithAccountsAsync(int chatId);
        public Task<Chat> AddAccountToChatByChatId(int chatId, Account account);

    }
}
