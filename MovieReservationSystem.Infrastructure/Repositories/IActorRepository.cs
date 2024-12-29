using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.GenericBases;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public interface IActorRepository : IGenericRepositoryAsync<Actor>
    {
        Task DeleteWithPersonAsync(Actor actor);
    }
}
