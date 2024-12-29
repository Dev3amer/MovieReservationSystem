using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Context;
using MovieReservationSystem.Infrastructure.GenericBases;
using MovieReservationSystem.Infrastructure.Repositories;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class HallRepository : GenericRepositoryAsync<Hall>, IHallRepository
    {
        public HallRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}