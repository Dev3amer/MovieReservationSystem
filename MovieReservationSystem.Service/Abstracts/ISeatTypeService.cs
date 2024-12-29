using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface ISeatTypeService
    {
        Task<ICollection<SeatType>> GetAllAsync();
        Task<SeatType> GetByIdAsync(byte id);
        Task<SeatType> AddAsync(SeatType seatType);
        Task<SeatType> EditAsync(SeatType seatType);
        Task<bool> IsExistAsync(byte id);
        Task<bool> IsExistByNameAsync(string key);
        Task<bool> IsExistByNameExcludeItselfAsync(byte id, string key);
        Task<bool> DeleteAsync(SeatType seatType);
    }
}
