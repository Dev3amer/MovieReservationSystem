using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Movies.Commands.Models
{
    public class DeleteMovieCommand : IRequest<Response<bool>>
    {
        public int MovieId { get; set; }
    }
}
