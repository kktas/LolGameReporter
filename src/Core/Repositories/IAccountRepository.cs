using Core.Models;

namespace Core.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Task<Account?> GetAccountByNameTagLinePuuidServerWithChats(string gameName, string tagLine, string puuid, int serverId);
        public Task<List<Account>> GetAllAccountsWithServerWithChatsAsync();
    }
}
