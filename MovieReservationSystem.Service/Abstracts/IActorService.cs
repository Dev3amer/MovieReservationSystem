using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IActorService
    {
        Task<ICollection<Actor>> GetAllAsync();
        IQueryable<Actor> GetAllQueryable();
        Task<Actor> GetByIdAsync(int id);
        Task<Actor> AddAsync(Actor actor);
        Task<Actor> EditAsync(Actor actor);
        Task<bool> IsExistAsync(int id);
        Task<bool> IsExistByNameAsync(string firstName, string lastName);
        Task<bool> IsExistByNameExcludeItselfAsync(int id, string firstName, string lastName);
        Task<bool> DeleteAsync(Actor actor);
    }
}
