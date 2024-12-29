using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Context;
using MovieReservationSystem.Infrastructure.GenericBases;
using MovieReservationSystem.Infrastructure.Repositories;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class ShowTimeRepository : GenericRepositoryAsync<ShowTime>, IShowTimeRepository
    {
        public ShowTimeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}