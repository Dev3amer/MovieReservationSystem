using MovieReservationSystem.Core.Features.Reservations.Queries.Results.Shared;

namespace MovieReservationSystem.Core.Features.Users.Queries.Results
{
    public class GetUserReservationsHistoryResponse
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public decimal FinalPrice { get; set; }
        public string HallName { get; set; } = default!;
        public string PaymentStatus { get; set; } = default!;
        public ShowTimeInReservationResponse ShowTime { get; set; } = default!;
        public IEnumerable<SeatsInReservationResponse> Seats { get; set; } = default!;
    }
}
