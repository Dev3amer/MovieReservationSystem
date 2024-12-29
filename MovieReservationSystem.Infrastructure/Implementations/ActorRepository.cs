using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Context;
using MovieReservationSystem.Infrastructure.GenericBases;
using MovieReservationSystem.Infrastructure.Repositories;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class ActorRepository : GenericRepositoryAsync<Actor>, IActorRepository
    {
        public ActorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task DeleteWithPersonAsync(Actor actor)
        {
            _context.Set<Actor>().Remove(actor);
            _context.Set<Person>().Remove(actor.Person);
            await _context.SaveChangesAsync();
        }
    }
}