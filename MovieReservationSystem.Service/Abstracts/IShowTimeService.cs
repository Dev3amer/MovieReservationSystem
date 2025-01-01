using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IShowTimeService
    {
        Task<ICollection<ShowTime>> GetAllAsync();
        Task<ShowTime> GetByIdAsync(int showTime);
        Task<ShowTime> AddAsync(ShowTime showTime);
        Task<ShowTime> EditAsync(ShowTime showTime);
        Task<bool> IsExistAsync(int showTimeId);
        Task<bool> IsExistAndInFutureAsync(int showTimeId);
        Task<bool> IsExistInSameHallAsync(int hallId, TimeOnly startTime, TimeOnly endTime);
        Task<bool> IsExistInSameHallExcludeItselfAsync(int showTimeId, int hallId, TimeOnly startTime, TimeOnly endTime);
        Task<bool> DeleteAsync(ShowTime showTime);
    }
}
