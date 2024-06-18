using Core;
using Core.Models;
using Core.Services.Database;

namespace Services.Database
{
    public class RegionService : IRegionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Region>> GetAllRegions()
        {
            return await _unitOfWork.RegionRepository.GetAllAsync();
        }

        public async Task<Region> GetRegionById(int id)
        {
            return await _unitOfWork.RegionRepository.GetByIdAsync(id);
        }

        public async Task<Region> GetRegionByName(string name)
        {
            return await _unitOfWork.RegionRepository.SingleOrDefaultAsync(x => x.Name == name);
        }
    }
}
