using Core;
using Core.Models;
using Core.Services.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Database
{
    public class ChampionService : IChampionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChampionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Champion>> GetAllChampions()
        {
            return await _unitOfWork.ChampionRepository.GetAllAsync();
        }

        public async Task<Champion> GetChampionById(int id)
        {
            return await _unitOfWork.ChampionRepository.GetByIdAsync(id);
        }

        public async Task<Champion> GetByChampionId(int id)
        {
            return await _unitOfWork.ChampionRepository.GetByChampionId(id);
        }

        public async Task<Champion> GetChampionByName(string name)
        {
            return await _unitOfWork.ChampionRepository.SingleOrDefaultAsync(x => x.Name == name);
        }
    }
}
