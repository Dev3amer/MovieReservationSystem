using MediatR;
using MovieReservationSystem.Core.Features.Movies.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Movies.Queries.Models
{
    public class GetMovieByIdQuery : IRequest<Response<GetMovieByIdResponse>>
    {
        public int Id { get; set; }
    }
}
