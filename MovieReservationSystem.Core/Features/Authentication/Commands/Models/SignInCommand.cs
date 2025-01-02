using MediatR;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Helpers;

namespace MovieReservationSystem.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtAuthTokenResponse>>
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
