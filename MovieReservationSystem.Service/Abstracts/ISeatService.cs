using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface ISeatService
    {
        Task<ICollection<Seat>> GetAllAsync();
        IQueryable<Seat> GetAllQueryable();
        Task<Seat> GetByIdAsync(int id);
        Task<Seat> AddAsync(Seat seat);
        Task<bool> IsExistAsync(int id);
        Task<bool> IsExistInHallAsync(string seatNumber, int hallId);
        Task<bool> IsExistBySeatIdInHallAsync(int seatId, int hallId);
        Task<bool> DeleteAsync(Seat seat);
        Task<int> CountSeatsInHall(int hallId);
        decimal CalculateSeatsPrice(IEnumerable<Seat> seatsList);

    }
}
