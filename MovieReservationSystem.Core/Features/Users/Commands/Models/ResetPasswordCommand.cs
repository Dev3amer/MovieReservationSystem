using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Users.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<bool>>
    {
        public string ResetCode { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
