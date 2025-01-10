using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Users.Commands.Models
{
    public class ValidatePasswordResetCodeCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; } = default!;
        public string ResetCode { get; set; } = default!;
    }
}
