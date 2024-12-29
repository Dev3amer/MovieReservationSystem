using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Repositories;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class HallService : IHallService
    {
        #region Fields
        private readonly IHallRepository _hallRepository;
        #endregion

        #region Constructors
        public HallService(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }
        #endregion

        #region Methods
        public async Task<ICollection<Hall>> GetAllAsync()
        {
            return await _hallRepository.GetTableNoTracking()
                .ToListAsync();
        }

        public async Task<Hall> GetByIdAsync(int id)
        {
            return await _hallRepository.GetTableAsTracking()
                .FirstOrDefaultAsync(h => h.HallId == id);
        }

        public async Task<Hall> AddAsync(Hall hall)
        {
            return await _hallRepository.AddAsync(hall);
        }

        public async Task<Hall> EditAsync(Hall hall)
        {
            return await _hallRepository.UpdateAsync(hall);
        }

        public async Task<bool> IsExistByIdAsync(int id)
        {
            return await _hallRepository.GetTableNoTracking().AnyAsync(h => h.HallId == id);
        }
        public async Task<bool> DeleteAsync(Hall hall)
        {
            _hallRepository.BeginTransaction();
            try
            {
                await _hallRepository.DeleteAsync(hall);
                _hallRepository.Commit();
                return true;
            }
            catch
            {
                _hallRepository.RollBack();
                return false;
            }
        }
        public async Task<bool> IsExistByNameAsync(string key)
        {
            return await _hallRepository.GetTableNoTracking().AnyAsync(h => h.Name == key);
        }
        public async Task<bool> IsExistByNameExcludeItselfAsync(int id, string key)
        {
            return await _hallRepository.GetTableNoTracking().AnyAsync(h => h.Name == key && h.HallId != id);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _hallRepository.GetTableNoTracking().AnyAsync(h => h.HallId == id);
        }
        #endregion
    }
}