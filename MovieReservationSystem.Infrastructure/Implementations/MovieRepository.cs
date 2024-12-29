using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Context;
using MovieReservationSystem.Infrastructure.GenericBases;
using MovieReservationSystem.Infrastructure.Repositories;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class MovieRepository : GenericRepositoryAsync<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}