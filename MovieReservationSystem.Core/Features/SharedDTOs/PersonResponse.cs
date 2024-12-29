namespace MovieReservationSystem.Core.Features.SharedDTOs
{
    public class PersonResponse
    {
        public string FullName { get; set; } = default!;
        public string? ImageURL { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;
    }
}
