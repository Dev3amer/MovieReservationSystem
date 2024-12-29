using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Repositories;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class GenreService : IGenreService
    {
        #region Fields
        private readonly IGenreRepository _genreRepository;
        #endregion

        #region Constructors
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        #endregion

        #region Methods
        public IQueryable<Genre> GetAllQueryable()
        {
            return _genreRepository.GetTableAsTracking().AsQueryable();
        }

        public async Task<Genre> GetByIdAsync(byte id)
        {
            return await _genreRepository.GetByIdAsync(id);
        }

        public async Task<bool> IsExistAsync(byte key)
        {
            return await _genreRepository.GetTableNoTracking().AnyAsync(g => g.GenreId == key);
        }
        public async Task<Genre> AddAsync(Genre genre)
        {
            return await _genreRepository.AddAsync(genre);
        }

        public async Task<Genre> EditAsync(Genre genre)
        {
            return await _genreRepository.UpdateAsync(genre);
        }

        public async Task<bool> DeleteAsync(Genre genre)
        {
            _genreRepository.BeginTransaction();
            try
            {
                await _genreRepository.DeleteAsync(genre);
                _genreRepository.Commit();
                return true;
            }
            catch
            {
                _genreRepository.RollBack();
                return false;
            }
        }

        public async Task<bool> IsExistByNameAsync(string key)
        {
            return await _genreRepository.GetTableNoTracking().AnyAsync(g => g.Name == key);
        }

        public async Task<bool> IsExistByNameExcludeItselfAsync(int id, string key) // 1  Drama
        {
            return await _genreRepository.GetTableNoTracking().AnyAsync(g => g.Name == key && g.GenreId != id);
        }
        #endregion
    }
}