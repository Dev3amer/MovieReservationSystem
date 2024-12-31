using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<string> GetJwtToken(User user);
    }
}
