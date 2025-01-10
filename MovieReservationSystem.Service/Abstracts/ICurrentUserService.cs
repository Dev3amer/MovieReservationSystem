using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface ICurrentUserService
    {
        Task<User> GetUserAsync();
        string GetUserId();
        Task<bool> CheckIfRuleExist(string roleName);
    }
}
