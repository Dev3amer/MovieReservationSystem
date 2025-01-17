using MovieReservationSystem.Core.Features.Reservations.Queries.Results.Shared;

namespace MovieReservationSystem.Core.Features.Payments.Queries.Results
{
    public class CreateOrUpdatePaymentIntentResult
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string PaymentIntentId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
        public string HallName { get; set; } = default!;
        public ShowTimeInReservationResponse ShowTime { get; set; } = default!;
        public ICollection<SeatsInReservationResponse> Seats { get; set; } = default!;
        public UserInReservationResponse User { get; set; } = default!;
    }
}
