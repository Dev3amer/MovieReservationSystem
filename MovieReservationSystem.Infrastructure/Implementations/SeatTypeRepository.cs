using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.Context;
using MovieReservationSystem.Infrastructure.GenericBases;
using MovieReservationSystem.Infrastructure.Repositories;

namespace MovieReservationSystem.Infrastructure.Implementations
{
    public class SeatTypeRepository : GenericRepositoryAsync<SeatType>, ISeatTypeRepository
    {
        public SeatTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}