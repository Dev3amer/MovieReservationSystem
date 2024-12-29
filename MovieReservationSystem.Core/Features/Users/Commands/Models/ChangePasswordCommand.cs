using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Users.Commands.Models
{
    public class ChangePasswordCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; } = default!;
        public string OldPassword { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
