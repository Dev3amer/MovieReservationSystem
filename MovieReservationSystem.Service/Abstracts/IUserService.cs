using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Service.Abstracts
{
    public interface IUserService
    {
        Task<User> CreateUser(User user, string password);
        Task SendConfirmUserEmailToken(User user);
        Task ConfirmUserEmail(User user, string code);
        Task<bool> SendResetUserPasswordCode(string email);
        Task<bool> ValidatePasswordResetCode(string email, string code);
        Task ResetUserPassword(string email, string newPassword);
    }
}
