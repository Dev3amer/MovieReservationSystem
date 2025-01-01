using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Repositories;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class ReservationService : IReservationService
    {
        #region Fields
        private readonly IReservationRepository _reservationRepository;
        #endregion

        #region Constructors
        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }


        #endregion

        #region Methods
        public async Task<ICollection<Reservation>> GetAllAsync()
        {
            return await _reservationRepository.GetTableNoTracking()
            .Include(r => r.User)
            .Include(r => r.ShowTime)
                .ThenInclude(st => st.Movie) // Include Movie from ShowTime
            .Include(r => r.ShowTime)
                .ThenInclude(st => st.Hall)  // Include Hall from ShowTime
            .Include(r => r.Seats)
            .AsSplitQuery()
            .OrderBy(r => r.ReservationDate)
            .ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _reservationRepository.GetTableAsTracking()
                .Include(r => r.User)
                .Include(r => r.ShowTime)
                    .ThenInclude(st => st.Movie) // Include Movie from ShowTime
                .Include(r => r.ShowTime)
                    .ThenInclude(st => st.Hall)  // Include Hall from ShowTime
                .Include(r => r.Seats)
                .AsSplitQuery()
                .FirstOrDefaultAsync(r => r.ReservationId == id);
        }

        public IQueryable<Reservation> GetAllQueryable(DateOnly? search)
        {

            var queryableList = _reservationRepository.GetTableNoTracking()
               .Include(r => r.User)
               .Include(r => r.ShowTime)
                   .ThenInclude(st => st.Movie) // Include Movie from ShowTime
               .Include(r => r.ShowTime)
                   .ThenInclude(st => st.Hall)  // Include Hall from ShowTime
               .Include(r => r.Seats)
               .AsSplitQuery()
               .OrderBy(r => r.ReservationDate)
               .AsQueryable();


            if (search != null)
            {
                var dateTimeFromSearch = new DateTime(search.Value.Year, search.Value.Month, search.Value.Day);

                queryableList = queryableList.Where(m => m.ReservationDate.Date.Equals(dateTimeFromSearch));
            }
            return queryableList;
        }

        public async Task<Reservation> AddAsync(Reservation newReservation)
        {
            return await _reservationRepository.AddAsync(newReservation);
        }

        public async Task<Reservation> EditAsync(Reservation mappedReservation)
        {
            return await _reservationRepository.UpdateAsync(mappedReservation);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _reservationRepository.GetTableNoTracking().AnyAsync(r => r.ReservationId == id);
        }
        public async Task<bool> DeleteAsync(Reservation reservation)
        {
            _reservationRepository.BeginTransaction();
            try
            {
                await _reservationRepository.DeleteAsync(reservation);
                _reservationRepository.Commit();
                return true;
            }
            catch
            {
                _reservationRepository.RollBack();
                return false;
            }
        }
        public async Task<bool> IsSeatReservedInSameShowTimeAsync(int seatId, int showTimeId)
        {
            //if seat in reservationSeats == seatId and showTimeId in Reservation == showTimeId

            return await _reservationRepository.GetTableNoTracking()
                .Include(r => r.ShowTime)
                .Include(r => r.Seats)
                .Where(r => r.ShowTime.ShowTimeId == showTimeId)
                .AnyAsync(r => r.Seats.Any(rs => rs.SeatId == seatId));
        }
        public decimal CalculateReservationPrice(IEnumerable<Seat> seatsList, decimal showTimePrice)
        {
            decimal price = 0;
            foreach (var seat in seatsList)
            {
                price += seat.SeatType.SeatTypePrice;
            }
            price = price + (showTimePrice * seatsList.Count());
            return price;
        }
        //public async Task<bool> IsExistByNameAsync(string key)
        //{
        //    return await _reservationRepository.GetTableNoTracking().AnyAsync(m => m.Title == key);
        //}

        //public async Task<bool> IsExistByNameExcludeItselfAsync(int id, string key)
        //{
        //    return await _reservationRepository.GetTableNoTracking().AnyAsync(m => m.Title == key && m.MovieId != id);

        //}
        #endregion
    }
}