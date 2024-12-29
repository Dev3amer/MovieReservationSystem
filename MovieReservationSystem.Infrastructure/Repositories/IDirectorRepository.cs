using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.GenericBases;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public interface IDirectorRepository : IGenericRepositoryAsync<Director>
    {
        Task DeleteWithPersonAsync(Director director);

    }
}
