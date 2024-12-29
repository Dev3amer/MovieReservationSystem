namespace MovieReservationSystem.Core.Features.Movies.Queries.Results.Shared
{
    public class DirectorInMovieResponse
    {
        public int DirectorId { get; set; }
        public string DirectorName { get; set; } = default!;
    }
}
