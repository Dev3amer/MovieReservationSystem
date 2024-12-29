namespace MovieReservationSystem.Core.Features.Genres.Queries.Results
{
    public class GetAllGenresResponse
    {
        public byte GenreId { get; set; }
        public string Name { get; set; } = default!;
    }
}
