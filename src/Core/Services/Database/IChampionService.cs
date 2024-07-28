using Core.Models;

namespace Core.Services.Database
{
    public interface IChampionService
    {
        public Task<IEnumerable<Champion>> GetAllChampions();
        public Task<Champion> GetChampionById(int id);
        public Task<Champion> GetByChampionId(int id);
        public Task<Champion> GetChampionByName(string name);
    }
}