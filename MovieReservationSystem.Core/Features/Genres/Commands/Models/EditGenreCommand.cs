using MediatR;
using MovieReservationSystem.Core.Features.Genres.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Genres.Commands.Models
{
    public class EditGenreCommand : IRequest<Response<GetGenreByIdResponse>>
    {
        public byte GenreId { get; set; }
        public string Name { get; set; } = default!;

        public EditGenreCommand(byte genreId, string name)
        {
            GenreId = genreId;
            Name = name.Trim();
        }
    }
}
