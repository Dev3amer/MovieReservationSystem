using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Users.Commands.Models
{
    public class RequestPasswordResetCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; } = default!;
    }
}
