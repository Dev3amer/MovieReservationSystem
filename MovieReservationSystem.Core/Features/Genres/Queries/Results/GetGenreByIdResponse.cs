namespace MovieReservationSystem.Core.Features.Genres.Queries.Results
{
    public class GetGenreByIdResponse
    {
        public byte GenreId { get; set; }
        public string Name { get; set; } = default!;
    }
}
