using Core.Repositories;

namespace Core
{
    public interface IUnitOfWork
    {
        IChatRepository ChatRepository { get; }
        IAccountRepository AccountRepository { get; }
        Task<int> CommitAsync();
    }
}
