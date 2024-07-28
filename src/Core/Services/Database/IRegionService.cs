using Core.Models;

namespace Core.Services.Database
{
    public interface IRegionService
    {
        public Task<Region> GetRegionById(int id);
        public Task<Region> GetRegionByName(string name);
        public Task<IEnumerable<Region>> GetAllRegions();
    }
}
