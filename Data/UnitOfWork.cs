using Core;
using Core.Repositories;
using Core.Services.Cache;
using Data.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LolGameReporterDbContext _context;
        private readonly IRedisCacheService cache;
        private readonly AccountRepository _accountRepository;
        private readonly ChatRepository _chatRepository;
        private readonly RegionRepository _regionRepository;
        private readonly ServerRepository _serverRepository;
        private readonly ChampionRepository _championRepository;

        public UnitOfWork(LolGameReporterDbContext context, IRedisCacheService cache)
        {
            this.cache = cache;
            _context = context;
            _accountRepository = _accountRepository ??= new AccountRepository(_context, cache);
            _chatRepository = _chatRepository ??= new ChatRepository(_context, cache);
            _regionRepository = _regionRepository ??= new RegionRepository(_context, cache);
            _serverRepository = _serverRepository ??= new ServerRepository(_context, cache);
            _championRepository = _championRepository ??= new ChampionRepository(_context, cache);
        }

        public IChatRepository ChatRepository => _chatRepository;

        public IAccountRepository AccountRepository => _accountRepository;
        public IRegionRepository RegionRepository => _regionRepository;
        public IServerRepository ServerRepository => _serverRepository;
        public IChampionRepository ChampionRepository => _championRepository;

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
