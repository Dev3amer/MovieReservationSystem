using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Genres.Commands.Models
{
    public class DeleteGenreCommand : IRequest<Response<bool>>
    {
        public byte GenreId { get; set; }
    }
}
