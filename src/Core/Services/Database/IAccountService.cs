using Core.Models;

namespace Core.Services.Database
{
    public interface IAccountService
    {
        public Task<Account> GetAccountById(int id);
        public Task<Account> GetAccountByTag(string tag);
        public Task<IEnumerable<Account>> GetAllAccounts();
        public Task<IEnumerable<Account>> GetAllAccountsWithServerWithChats();
        public Task<IEnumerable<Account>> GetAllAccountsByChatId(int chatId);
        public Task<IEnumerable<Account>> GetAllAccountsByTelegramChatId(long telegramChatId);
        public Task<Account> CreateAccount(Account account);
        public Task DeleteAccount(int id, long deletedById, string deletedByName);
        public Task<Account> ChangeAccountGameNameTagLine(Account account, string gameName, string tagLine);
    }
}
