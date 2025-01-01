using MediatR;
using MovieReservationSystem.Core.Features.Movies.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Movies.Queries.Models
{
    public class GetAllMoviesQuery : IRequest<Response<List<GetAllMoviesResponse>>>
    {

    }
}
