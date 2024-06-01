using Core;
using Core.Repositories;
using Data.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LolGameReporterDbContext _context;
        private readonly IDistributedCache cache;
        private readonly AccountRepository _accountRepository;
        private readonly ChatRepository _chatRepository;

        public UnitOfWork(LolGameReporterDbContext context, IDistributedCache cache)
        {
            this.cache = cache;
            _context = context;
            _accountRepository = _accountRepository ??= new AccountRepository(_context, cache);
            _chatRepository = _chatRepository ??= new ChatRepository(_context, cache);
        }

        public IChatRepository ChatRepository => _chatRepository;

        public IAccountRepository AccountRepository => _accountRepository;

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
