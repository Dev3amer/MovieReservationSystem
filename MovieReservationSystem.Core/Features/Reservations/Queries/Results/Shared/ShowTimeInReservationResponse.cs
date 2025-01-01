namespace MovieReservationSystem.Core.Features.Reservations.Queries.Results.Shared
{
    public class ShowTimeInReservationResponse
    {
        public int ShowTimeId { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string MovieName { get; set; } = default!;
    }
}
