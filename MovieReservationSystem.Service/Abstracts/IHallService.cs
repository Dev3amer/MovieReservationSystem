using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IHallService
    {
        Task<ICollection<Hall>> GetAllAsync();
        Task<Hall> GetByIdAsync(int id);
        Task<Hall> AddAsync(Hall hall);
        Task<Hall> EditAsync(Hall hall);
        Task<bool> IsExistAsync(int id);
        Task<bool> IsExistByNameAsync(string key);
        Task<bool> IsExistByNameExcludeItselfAsync(int id, string key);
        Task<bool> DeleteAsync(Hall hall);
    }
}
