using Core.Models;

namespace Core.Services
{
    public interface IAccountService
    {
        public Task<Account> GetAccountById(int id);
        public Task<Account> GetAccountByTag(string tag);
        public Task<IEnumerable<Account>> GetAllAccounts();
        public Task<IEnumerable<Account>> GetAllAccountsByChatId(int chatID);
        public Task<IEnumerable<Account>> GetAllAccountsByTelegramChatId(long telegramChatId);
        public Task<Account> CreateAccount(Account account);
        public Task DeleteAccount(int id, long deletedById, string deletedByName);
    }
}
