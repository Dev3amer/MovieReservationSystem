using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Context;
using MovieReservationSystem.Infrastructure.GenericBases;
using MovieReservationSystem.Infrastructure.Repositories;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class SeatRepository : GenericRepositoryAsync<Seat>, ISeatRepository
    {
        public SeatRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}