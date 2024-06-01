using Core;
using Core.Models;
using Core.Services.Database;

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
            await _unitOfWork.AccountRepository.AddAsync(account);
            await _unitOfWork.CommitAsync();

            return account;
        }

        public async Task DeleteAccount(int id, long deletedById, string deletedByName)
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(id);
            _unitOfWork.AccountRepository.Delete(account, deletedById, deletedByName);

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
            return await _unitOfWork.AccountRepository.FindAsync(a => a.ChatId == chatId);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsByTelegramChatId(long telegramChatId)
        {
            return await _unitOfWork.AccountRepository.FindAsync(a => a.Chat.TelegramChatId == telegramChatId);
        }
    }
}
