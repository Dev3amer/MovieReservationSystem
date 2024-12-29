using MovieReservationSystem.Core.Features.Seats.Queries.Results.Shared;

namespace MovieReservationSystem.Core.Features.Seats.Queries.Results
{
    public class GetAllSeatsResponse
    {
        public int SeatId { get; set; }
        public string SeatNumber { get; set; } = default!;
        public HallInSeatResponse Hall { get; set; }
        public SeatTypeInSeatResponse SeatType { get; set; }
    }

}
