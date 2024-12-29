using MediatR;
using MovieReservationSystem.Core.Features.Genres.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Genres.Commands.Models
{
    public class CreateGenreCommand : IRequest<Response<GetGenreByIdResponse>>
    {
        public string Name { get; set; } = default!;

        public CreateGenreCommand(string name)
        {
            Name = name.Trim();
        }
    }
}
