using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IGenreService
    {
        IQueryable<Genre> GetAllQueryable();
        Task<Genre> GetByIdAsync(byte id);
        Task<bool> IsExistAsync(byte key);
        Task<Genre> AddAsync(Genre genre);
        Task<Genre> EditAsync(Genre genre);
        Task<bool> DeleteAsync(Genre genre);
        Task<bool> IsExistByNameAsync(string key);
        Task<bool> IsExistByNameExcludeItselfAsync(int id, string key);
    }
}
