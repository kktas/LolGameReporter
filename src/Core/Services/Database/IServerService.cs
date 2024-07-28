using Core.Models;

namespace Core.Services.Database
{
    public interface IServerService
    {
        public Task<Server> GetServerById(int id);
        public Task<Server> GetServerByName(string name);
        public Task<IEnumerable<Server>> GetAllServers();
    }
}
