using MediatR;
using MovieReservationSystem.Core.Features.Movies.Queries.Results;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Movies.Commands.Models
{
    public class EditMovieCommand : IRequest<Response<GetMovieByIdResponse>>
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PosterURL { get; set; } = default!;
        public int DurationInMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public int DirectorId { get; set; }
        public List<int>? ActorsIds { get; set; }
        public List<byte> GenreIds { get; set; } = new();

        public EditMovieCommand(int movieId, string title, string description, string posterURL, int durationInMinutes, int releaseYear, decimal rate, bool isActive, int directorId, List<int> actorsIds, List<byte> genreIds)
        {
            MovieId = movieId;
            Title = title.Trim();
            Description = description.Trim();
            PosterURL = posterURL;
            DurationInMinutes = durationInMinutes;
            ReleaseYear = releaseYear;
            Rate = rate;
            IsActive = isActive;
            DirectorId = directorId;
            ActorsIds = actorsIds;
            GenreIds = genreIds;
        }
    }
}
