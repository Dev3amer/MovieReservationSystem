namespace MovieReservationSystem.Core.Features.Reservations.Queries.Results.Shared
{
    public class SeatsInReservationResponse
    {
        public int SeatId { get; set; }
        public string SeatNumber { get; set; } = default!;
    }
}
