using Core;
using Core.Models;
using Core.Services.Database;
using System.Diagnostics;

namespace Services.Database
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Account> CreateAccount(Account account)
        {

            var existingAccount = await _unitOfWork.AccountRepository.GetAccountByNameTagLinePuuidServerWithChats(account.GameName, account.TagLine, account.Puuid, account.ServerId);

            if (existingAccount is not null)
            {
                if (existingAccount.Chats.Contains(account.Chats[0])) throw new Exception("The account already exists in this chat.");
                existingAccount.Chats.AddRange(account.Chats);
            }
            else
            {
                Chat accountChat = account.Chats[0];
                account.Chats = [];

                await _unitOfWork.AccountRepository.AddAsync(account);
                await _unitOfWork.ChatRepository.AddAccountToChatByChatId(accountChat.Id, account);
            }

            await _unitOfWork.CommitAsync();
            return account;
        }

        public async Task DeleteAccount(int id, long deletedById, string deletedByName)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            _unitOfWork.AccountRepository.DeleteAsync(account, deletedById, deletedByName);

            await _unitOfWork.CommitAsync();
        }

        public async Task<Account> GetAccountById(int id)
        {
            return await _unitOfWork.AccountRepository.GetByIdAsync(id);
        }

        public async Task<Account> GetAccountByTag(string tag)
        {
            return await _unitOfWork.AccountRepository.SingleOrDefaultAsync(a => $"{a.GameName}#{a.TagLine}" == tag);
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _unitOfWork.AccountRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Account>> GetAllAccountsByChatId(int chatId)
        {
            return await _unitOfWork.AccountRepository.FindAsync(a => a.Chats.Any(c => c.Id == chatId));
        }

        public async Task<IEnumerable<Account>> GetAllAccountsByTelegramChatId(long telegramChatId)
        {
            return await _unitOfWork.AccountRepository.FindAsync(a => a.Chats.Any(c => c.TelegramChatId == telegramChatId));
        }
        public async Task<IEnumerable<Account>> GetAllAccountsWithServerWithChats()
        {
            return await _unitOfWork.AccountRepository.GetAllAccountsWithServerWithChatsAsync();
        }

        public async Task<Account> ChangeAccountGameNameTagLine(Account account, string gameName, string tagLine)
        {
            account.GameName = gameName;
            account.TagLine = tagLine;

            await _unitOfWork.CommitAsync();

            return account;
        }
    }
}
