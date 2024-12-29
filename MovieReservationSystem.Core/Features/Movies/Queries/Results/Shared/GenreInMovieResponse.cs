namespace MovieReservationSystem.Core.Features.Movies.Queries.Results.Shared
{
    public class GenreInMovieResponse
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = default!;
    }
}
