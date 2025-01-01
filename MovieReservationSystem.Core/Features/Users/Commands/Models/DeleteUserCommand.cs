using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Users.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; } = default!;
    }
}
