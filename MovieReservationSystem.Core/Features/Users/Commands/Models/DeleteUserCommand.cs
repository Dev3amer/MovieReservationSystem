using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Users.Commands.Models
{
    public class DeleteUserCommand : IRequest<Response<bool>>
    {
        public string Id { get; set; } = default!;
    }
}
