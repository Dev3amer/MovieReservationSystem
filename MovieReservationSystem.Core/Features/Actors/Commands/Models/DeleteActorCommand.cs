using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Actors.Commands.Models
{
    public class DeleteActorCommand : IRequest<Response<bool>>
    {
        public int ActorId { get; set; }
    }
}
