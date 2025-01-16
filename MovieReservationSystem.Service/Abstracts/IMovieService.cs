using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Data.Helpers;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        IQueryable<Movie> GetAllQueryable(string search, MovieOrderingEnum? movieOrderingEnum);
        Task<Movie> AddAsync(Movie newMovie);
        Task<Movie> EditAsync(Movie mappedMovie);
        Task<bool> IsExistAsync(int id);
        Task<bool> IsExistByNameAsync(string key);
        Task<bool> IsExistByNameExcludeItselfAsync(int id, string key);
        Task<bool> DeleteAsync(Movie Movie);
    }
}
