using Microsoft.EntityFrameworkCore.Storage;

namespace MovieReservationSystem.Infrastructure.GenericBases
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(ICollection<T> entities);
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        Task<int> SaveChangesAsync();
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateRangeAsync(ICollection<T> entities);
    }
}
