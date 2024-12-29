using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Repositories;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class SeatTypeService : ISeatTypeService
    {
        #region Fields
        private readonly ISeatTypeRepository _seatTypeRepository;
        #endregion

        #region Constructors
        public SeatTypeService(ISeatTypeRepository seatTypeRepository)
        {
            _seatTypeRepository = seatTypeRepository;
        }
        #endregion

        #region Methods
        public async Task<ICollection<SeatType>> GetAllAsync()
        {
            return await _seatTypeRepository.GetTableNoTracking()
                .ToListAsync();
        }

        public async Task<SeatType> GetByIdAsync(byte id)
        {
            return await _seatTypeRepository.GetTableAsTracking()
                .FirstOrDefaultAsync(st => st.SeatTypeId == id);
        }

        public async Task<SeatType> AddAsync(SeatType seatType)
        {
            return await _seatTypeRepository.AddAsync(seatType);
        }

        public async Task<SeatType> EditAsync(SeatType seatType)
        {
            return await _seatTypeRepository.UpdateAsync(seatType);
        }

        public async Task<bool> IsExistAsync(byte id)
        {
            return await _seatTypeRepository.GetTableNoTracking().AnyAsync(st => st.SeatTypeId == id);
        }
        public async Task<bool> DeleteAsync(SeatType seatType)
        {
            _seatTypeRepository.BeginTransaction();
            try
            {
                await _seatTypeRepository.DeleteAsync(seatType);
                _seatTypeRepository.Commit();
                return true;
            }
            catch
            {
                _seatTypeRepository.RollBack();
                return false;
            }
        }
        public async Task<bool> IsExistByNameAsync(string key)
        {
            return await _seatTypeRepository.GetTableNoTracking().AnyAsync(st => st.TypeName == key);
        }
        public async Task<bool> IsExistByNameExcludeItselfAsync(byte id, string key)
        {
            return await _seatTypeRepository.GetTableNoTracking().AnyAsync(st => st.TypeName == key && st.SeatTypeId != id);
        }
        #endregion
    }
}