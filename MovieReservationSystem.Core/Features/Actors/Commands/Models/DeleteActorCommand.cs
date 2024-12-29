using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Actors.Commands.Models
{
    public class DeleteActorCommand : IRequest<Response<bool>>
    {
        public int ActorId { get; set; }
    }
}
