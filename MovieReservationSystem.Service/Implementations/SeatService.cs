using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Repositories;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class SeatService : ISeatService
    {
        #region Fields
        private readonly ISeatRepository _seatRepository;
        #endregion

        #region Constructors
        public SeatService(ISeatRepository seatRepository)
        {
            _seatRepository = seatRepository;
        }
        #endregion


        #region Methods
        public async Task<ICollection<Seat>> GetAllAsync()
        {
            return await _seatRepository.GetTableNoTracking()
                .Include(s => s.SeatType)
                .Include(s => s.Hall)
                .ToListAsync();
        }

        public async Task<Seat> GetByIdAsync(int id)
        {
            return await _seatRepository.GetTableAsTracking()
                .Include(s => s.SeatType)
                .Include(s => s.Hall)
                .FirstOrDefaultAsync(s => s.SeatId == id);
        }

        public async Task<Seat> AddAsync(Seat seat)
        {
            return await _seatRepository.AddAsync(seat);
        }

        public async Task<bool> DeleteAsync(Seat seat)
        {
            _seatRepository.BeginTransaction();
            try
            {
                await _seatRepository.DeleteAsync(seat);
                _seatRepository.Commit();
                return true;
            }
            catch
            {
                _seatRepository.RollBack();
                return false;
            }
        }

        public async Task<bool> IsExistInHallAsync(string seatNumber, int hallId)
        {
            return await _seatRepository.GetTableNoTracking()
                .AnyAsync(s => s.SeatNumber == seatNumber && s.Hall.HallId == hallId);

        }

        public async Task<int> CountSeatsInHall(int hallId)
        {
            return await _seatRepository.GetTableNoTracking()
                .Include(s => s.Hall)
                .Where(s => s.HallId == hallId)
                .CountAsync();
        }
        #endregion
    }
}