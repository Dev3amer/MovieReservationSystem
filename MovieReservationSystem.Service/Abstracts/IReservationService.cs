using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IReservationService
    {
        Task<ICollection<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        IQueryable<Reservation> GetAllQueryable(DateOnly? search);
        Task<Reservation> AddAsync(Reservation newReservation);
        Task<Reservation> EditAsync(Reservation mappedReservation);
        Task<bool> IsExistAsync(int id);
        Task<bool> IsSeatReservedInSameShowTimeAsync(int seatId, int showTimeId);
        public decimal CalculateReservationPrice(IEnumerable<Seat> seatsList, decimal showTimePrice);
        //Task<bool> IsExistByNameAsync(string key);
        //Task<bool> IsExistByNameExcludeItselfAsync(int id, string key);
        Task<bool> DeleteAsync(Reservation reservation);
    }
}
