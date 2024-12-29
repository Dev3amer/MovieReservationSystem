using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IDirectorService
    {
        Task<ICollection<Director>> GetAllAsync();
        Task<Director> GetByIdAsync(int id);
        Task<Director> AddAsync(Director director);
        Task<Director> EditAsync(Director director);
        Task<bool> IsExistAsync(int id);
        Task<bool> IsExistByNameAsync(string firstName, string lastName);
        Task<bool> IsExistByNameExcludeItselfAsync(int id, string firstName, string lastName);
        Task<bool> DeleteAsync(Director director);
    }
}
