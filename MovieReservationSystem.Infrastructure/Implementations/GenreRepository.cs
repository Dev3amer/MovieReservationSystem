using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Context;
using MovieReservationSystem.Infrastructure.GenericBases;
using MovieReservationSystem.Infrastructure.Repositories;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class GenreRepository : GenericRepositoryAsync<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Genre> GetByIdAsync(byte id)
        {
            return await _context.Set<Genre>().FindAsync(id);
        }
    }
}
