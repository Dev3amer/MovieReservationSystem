using MovieReservationSystem.Data.Entities;
using MovieReservationSystem.Infrastructure.GenericBases;

namespace MovieReservationSystem.Infrastructure.Repositories
{
    public interface IGenreRepository : IGenericRepositoryAsync<Genre>
    {
        Task<Genre> GetByIdAsync(byte id);
    }
}
