using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Repositories;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class DirectorService : IDirectorService
    {
        #region Fields
        private readonly IDirectorRepository _directorRepository;
        #endregion

        #region Constructors
        public DirectorService(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }
        #endregion

        #region Methods
        public async Task<ICollection<Director>> GetAllAsync()
        {
            return await _directorRepository.GetTableNoTracking()
                .Include(d => d.Person)
                .ToListAsync();
        }

        public async Task<Director> GetByIdAsync(int id)
        {
            return await _directorRepository.GetTableAsTracking()
                .Include(d => d.Person)
                .FirstOrDefaultAsync(d => d.DirectorId == id);
        }

        public async Task<Director> AddAsync(Director director)
        {
            return await _directorRepository.AddAsync(director);
        }

        public async Task<Director> EditAsync(Director director)
        {
            return await _directorRepository.UpdateAsync(director);
        }

        public async Task<bool> IsExistByIdAsync(int id)
        {
            return await _directorRepository.GetTableNoTracking().AnyAsync(d => d.DirectorId == id);
        }
        public async Task<bool> DeleteAsync(Director director)
        {
            _directorRepository.BeginTransaction();
            try
            {
                await _directorRepository.DeleteWithPersonAsync(director);
                _directorRepository.Commit();
                return true;
            }
            catch
            {
                _directorRepository.RollBack();
                return false;
            }
        }
        public async Task<bool> IsExistByNameAsync(string firstName, string lastName)
        {
            string fullName = $"{firstName} {lastName}";
            return await _directorRepository.GetTableNoTracking().Include(d => d.Person)
                .AnyAsync(d => d.Person.FirstName + " " + d.Person.LastName == fullName);
        }
        public async Task<bool> IsExistByNameExcludeItselfAsync(int id, string firstName, string lastName)
        {
            string fullName = $"{firstName} {lastName}";
            return await _directorRepository.GetTableNoTracking().Include(d => d.Person)
                .AnyAsync(d => d.Person.FirstName + " " + d.Person.LastName == fullName && d.DirectorId != id);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _directorRepository.GetTableNoTracking()
                .AnyAsync(d => d.DirectorId == id);
        }
        #endregion
    }
}