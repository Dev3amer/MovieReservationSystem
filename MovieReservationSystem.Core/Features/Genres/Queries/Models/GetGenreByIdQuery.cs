using MediatR;
using MovieReservationSystem.Core.Features.Genres.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Genres.Queries.Models
{
    public class GetGenreByIdQuery : IRequest<Response<GetGenreByIdResponse>>
    {
        public byte GenreId { get; set; }
    }
}
