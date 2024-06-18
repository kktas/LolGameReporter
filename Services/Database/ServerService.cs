using Core;
using Core.Models;
using Core.Services.Database;

namespace Services.Database
{
    public class ServerService : IServerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Server>> GetAllServers()
        {
            return await _unitOfWork.ServerRepository.GetAllAsync();
        }

        public async Task<Server> GetServerById(int id)
        {
            return await _unitOfWork.ServerRepository.GetByIdAsync(id);
        }

        public async Task<Server> GetServerByName(string name)
        {
            return await _unitOfWork.ServerRepository.SingleOrDefaultAsync(x => x.Name == name);
        }
    }
}
