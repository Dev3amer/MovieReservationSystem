using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.GenericBases;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public interface IReservationRepository : IGenericRepositoryAsync<Reservation>
    {
    }
}
