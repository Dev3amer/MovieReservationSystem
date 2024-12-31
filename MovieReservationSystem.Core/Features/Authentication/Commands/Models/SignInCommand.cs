using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
