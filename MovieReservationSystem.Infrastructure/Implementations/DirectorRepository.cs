using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Context;
using MovieReservationSystem.Infrastructure.GenericBases;
using MovieReservationSystem.Infrastructure.Repositories;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class DirectorRepository : GenericRepositoryAsync<Director>, IDirectorRepository
    {
        public DirectorRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public async Task DeleteWithPersonAsync(Director director)
        {
            _context.Set<Director>().Remove(director);
            _context.Set<Person>().Remove(director.Person);
            await _context.SaveChangesAsync();
        }
    }
}
