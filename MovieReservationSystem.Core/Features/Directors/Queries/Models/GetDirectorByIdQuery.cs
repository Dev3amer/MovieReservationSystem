using MediatR;
using MovieReservationSystem.Core.Features.Directors.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Directors.Queries.Models
{
    public class GetDirectorByIdQuery : IRequest<Response<GetDirectorByIdResponse>>
    {
        public int DirectorId { get; set; }
    }
}
