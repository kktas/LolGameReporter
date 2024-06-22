using Core.Repositories;

namespace Core
{
    public interface IUnitOfWork
    {
        IChatRepository ChatRepository { get; }
        IAccountRepository AccountRepository { get; }
        IRegionRepository RegionRepository { get; }
        IServerRepository ServerRepository { get; }
        IChampionRepository ChampionRepository { get; }
        Task<int> CommitAsync();
    }
}
