using Microsoft.EntityFrameworkCore;
using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Repositories;
using MovieReservationSystem.Service.Abstracts;

namespace MovieReservationSystem.Service.Implementations
{
    public class ActorService : IActorService
    {
        #region Fields
        private readonly IActorRepository _actorRepository;
        #endregion

        #region Constructors
        public ActorService(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }
        #endregion

        #region Methods
        public async Task<ICollection<Actor>> GetAllAsync()
        {
            return await _actorRepository.GetTableNoTracking()
                .Include(a => a.Person)
                .ToListAsync();
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            return await _actorRepository.GetTableAsTracking()
                .Include(a => a.Person)
                .FirstOrDefaultAsync(a => a.ActorId == id);
        }

        public async Task<Actor> AddAsync(Actor actor)
        {
            return await _actorRepository.AddAsync(actor);
        }

        public async Task<Actor> EditAsync(Actor actor)
        {
            return await _actorRepository.UpdateAsync(actor);
        }

        public async Task<bool> IsExistByIdAsync(int id)
        {
            return await _actorRepository.GetTableNoTracking().AnyAsync(d => d.ActorId == id);
        }
        public async Task<bool> DeleteAsync(Actor actor)
        {
            _actorRepository.BeginTransaction();
            try
            {
                await _actorRepository.DeleteWithPersonAsync(actor);
                _actorRepository.Commit();
                return true;
            }
            catch
            {
                _actorRepository.RollBack();
                return false;
            }
        }
        public async Task<bool> IsExistByNameAsync(string firstName, string lastName)
        {
            string fullName = $"{firstName} {lastName}";
            return await _actorRepository.GetTableNoTracking().Include(d => d.Person)
                .AnyAsync(d => d.Person.FirstName + " " + d.Person.LastName == fullName);
        }
        public async Task<bool> IsExistByNameExcludeItselfAsync(int id, string firstName, string lastName)
        {
            string fullName = $"{firstName} {lastName}";
            return await _actorRepository.GetTableNoTracking().Include(d => d.Person)
                .AnyAsync(d => d.Person.FirstName + " " + d.Person.LastName == fullName && d.ActorId != id);
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return await _actorRepository.GetTableNoTracking()
                .AnyAsync(d => d.ActorId == id);
        }

        public IQueryable<Actor> GetAllQueryable()
        {
            return _actorRepository.GetTableAsTracking().Include(p => p.Person).AsQueryable();

        }
        #endregion
    }
}