using MediatR;
using Microsoft.AspNetCore.Http;
using MovieReservationSystem.Core.Features.Movies.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Movies.Commands.Models
{
    public class EditMovieCommand : IRequest<Response<GetMovieByIdResponse>>
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public IFormFile? Poster { get; set; }
        public int DurationInMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public int DirectorId { get; set; }
        public List<int>? ActorsIds { get; set; }
        public List<byte> GenreIds { get; set; } = new();

    }
}
