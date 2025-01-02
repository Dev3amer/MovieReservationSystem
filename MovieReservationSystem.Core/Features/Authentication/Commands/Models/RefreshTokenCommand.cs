using MediatR;
using MovieReservationSystem.Core.Response;
using MovieReservationSystem.Data.Helpers;

namespace MovieReservationSystem.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<Response<JwtAuthTokenResponse>>
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
