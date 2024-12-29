using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Data.Helpers;
using MovieReservationSystem.Infrastructure.Repositories;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class MovieService : IMovieService
    {
        #region Fields
        private readonly IMovieRepository _movieRepository;
        #endregion

        #region Constructors
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }


        #endregion

        #region Methods
        public async Task<ICollection<Movie>> GetAllAsync()
        {
            return await _movieRepository.GetTableNoTracking()
                .Include(m => m.Genres)
                .Include(m => m.ShowTimes)
                .Include(m => m.Actors).ThenInclude(a => a.Person)
                .Include(m => m.Director).ThenInclude(a => a.Person)
                .ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _movieRepository.GetTableAsTracking()
                .Include(m => m.Genres)
                .Include(m => m.ShowTimes)
                .Include(m => m.Actors).ThenInclude(a => a.Person)
                .Include(m => m.Director).ThenInclude(a => a.Person)
                .FirstOrDefaultAsync(m => m.MovieId == id);
        }

        public IQueryable<Movie> GetAllQueryable(string search, MovieOrderingEnum? movieOrderingEnum)
        {

            var queryableList = _movieRepository.GetTableNoTracking()
               .Include(m => m.Genres)
               .Include(m => m.ShowTimes)
               .Include(m => m.Actors).ThenInclude(a => a.Person)
               .Include(m => m.Director).ThenInclude(a => a.Person)
               .AsQueryable();

            if (search != null)
            {
                queryableList = queryableList.Where(m => m.Title.Contains(search));
            }

            switch (movieOrderingEnum)
            {
                case MovieOrderingEnum.MovieId:
                    queryableList = queryableList.OrderBy(m => m.MovieId);
                    break;
                case MovieOrderingEnum.Title:
                    queryableList = queryableList.OrderBy(m => m.Title);
                    break;
                case MovieOrderingEnum.ReleaseYear:
                    queryableList = queryableList.OrderByDescending(m => m.ReleaseYear);
                    break;
                case MovieOrderingEnum.Rate:
                    queryableList = queryableList.OrderByDescending(m => m.Rate);
                    break;
                default:
                    queryableList = queryableList.OrderBy(m => m.Title);
                    break;
            }

            return queryableList;
        }

        public async Task<Movie> AddAsync(Movie newMovie)
        {
            return await _movieRepository.AddAsync(newMovie);
        }

        public async Task<Movie> EditAsync(Movie mappedMovie)
        {
            return await _movieRepository.UpdateAsync(mappedMovie);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _movieRepository.GetTableNoTracking().AnyAsync(m => m.MovieId == id);
        }
        public async Task<bool> DeleteAsync(Movie Movie)
        {
            _movieRepository.BeginTransaction();
            try
            {
                await _movieRepository.DeleteAsync(Movie);
                _movieRepository.Commit();
                return true;
            }
            catch
            {
                _movieRepository.RollBack();
                return false;
            }
        }

        public async Task<bool> IsExistByNameAsync(string key)
        {
            return await _movieRepository.GetTableNoTracking().AnyAsync(m => m.Title == key);
        }

        public async Task<bool> IsExistByNameExcludeItselfAsync(int id, string key)
        {
            return await _movieRepository.GetTableNoTracking().AnyAsync(m => m.Title == key && m.MovieId != id);

        }
        #endregion
    }
}