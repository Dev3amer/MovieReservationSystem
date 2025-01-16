using MediatR;
using Microsoft.AspNetCore.Http;
using MovieReservationSystem.Core.Features.Movies.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Movies.Commands.Models
{
    public class CreateMovieCommand : IRequest<Response<GetMovieByIdResponse>>
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public IFormFile Poster { get; set; } = default!;
        public int DurationInMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public int DirectorId { get; set; }
        public List<int>? ActorsIds { get; set; }
        public List<byte> GenresIds { get; set; } = new();

        //public CreateMovieCommand(string title, string description, IFormFile posterURL, int durationInMinutes, int releaseYear, decimal rate, bool isActive, int directorId, List<int> actorsIds, List<byte> genresIds)
        //{
        //    Title = title.Trim();
        //    Description = description.Trim();
        //    Poster = posterURL;
        //    DurationInMinutes = durationInMinutes;
        //    ReleaseYear = releaseYear;
        //    Rate = rate;
        //    IsActive = isActive;
        //    DirectorId = directorId;
        //    ActorsIds = actorsIds;
        //    GenresIds = genresIds;
        //}
    }
}
