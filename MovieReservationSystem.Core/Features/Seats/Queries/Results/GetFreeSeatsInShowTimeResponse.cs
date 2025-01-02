namespace MovieReservationSystem.Core.Features.Seats.Queries.Results
{
    public class GetFreeSeatsInShowTimeResponse
    {
        public int SeatId { get; set; }
        public string SeatNumber { get; set; } = default!;
        public string SeatTypeName { get; set; } = default!;
    }
}
