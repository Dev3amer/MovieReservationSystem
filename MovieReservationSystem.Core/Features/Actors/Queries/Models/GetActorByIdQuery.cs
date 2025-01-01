using MediatR;
using MovieReservationSystem.Core.Features.Actors.Queries.Results;
using MovieReservationSystem.Core.Features.Directors.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Actors.Queries.Models
{
    public class GetActorByIdQuery : IRequest<Response<GetActorByIdResponse>>
    {
        public int ActorId { get; set; }
    }
}
