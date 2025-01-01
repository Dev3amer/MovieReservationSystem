using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Repositories;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class ShowTimeService : IShowTimeService
    {
        #region Fields
        private readonly IShowTimeRepository _showTimeRepository;
        #endregion

        #region Constructors
        public ShowTimeService(IShowTimeRepository showTimeRepository)
        {
            _showTimeRepository = showTimeRepository;
        }
        #endregion

        #region Methods
        public async Task<ICollection<ShowTime>> GetAllAsync()
        {
            return await _showTimeRepository.GetTableNoTracking()
                .Include(st => st.Movie)
                .Include(st => st.Hall)
                .ToListAsync();
        }

        public async Task<ShowTime> GetByIdAsync(int id)
        {
            return await _showTimeRepository.GetTableAsTracking()
                .Include(st => st.Movie)
                .Include(st => st.Hall)
                .FirstOrDefaultAsync(st => st.ShowTimeId == id);
        }

        public async Task<ShowTime> AddAsync(ShowTime showTime)
        {
            return await _showTimeRepository.AddAsync(showTime);
        }

        public async Task<ShowTime> EditAsync(ShowTime showTime)
        {
            return await _showTimeRepository.UpdateAsync(showTime);
        }

        public async Task<bool> IsExistByIdAsync(int id)
        {
            return await _showTimeRepository.GetTableNoTracking().AnyAsync(st => st.ShowTimeId == id);
        }
        public async Task<bool> DeleteAsync(ShowTime showTime)
        {
            _showTimeRepository.BeginTransaction();
            try
            {
                await _showTimeRepository.DeleteAsync(showTime);
                _showTimeRepository.Commit();
                return true;
            }
            catch
            {
                _showTimeRepository.RollBack();
                return false;
            }
        }
        public async Task<bool> IsExistInSameHallAsync(int hallId, TimeOnly startTime, TimeOnly endTime)
        {
            return await _showTimeRepository.GetTableNoTracking()
                .AnyAsync(st => st.Hall.HallId == hallId && startTime.IsBetween(st.StartTime, st.EndTime));
        }
        public async Task<bool> IsExistInSameHallExcludeItselfAsync(int showTimeId, int hallId, TimeOnly startTime, TimeOnly endTime)
        {
            return await _showTimeRepository.GetTableNoTracking()
                .AnyAsync(st => st.Hall.HallId == hallId && startTime.IsBetween(st.StartTime, st.EndTime) && st.ShowTimeId != showTimeId);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _showTimeRepository.GetTableNoTracking().AnyAsync(st => st.ShowTimeId == id);
        }

        public async Task<bool> IsExistAndInFutureAsync(int showTimeId)
        {
            var showTime = await _showTimeRepository.GetTableAsTracking().FirstOrDefaultAsync(st => st.ShowTimeId == showTimeId);
            return showTime.Day.ToDateTime(showTime.EndTime) > DateTime.Now;
        }
        #endregion
    }
}