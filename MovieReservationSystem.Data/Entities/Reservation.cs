using MovieReservationSystem.Data.Entities.Identity;
using MovieReservationSystem.Data.Helpers;

namespace MovieReservationSystem.Data.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public decimal FinalPrice { get; set; }

        //payment tracking
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public PaymentStatusEnum PaymentStatus { get; set; } = PaymentStatusEnum.Pending;

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int ShowTimeId { get; set; }
        public string UserId { get; set; } = default!;
        public virtual ShowTime ShowTime { get; set; } = new();
        public virtual ICollection<Seat> ReservedSeats { get; set; } = new HashSet<Seat>();
        public virtual User User { get; set; } = new();


        public DateTime AllowedTime { get; set; } = DateTime.Now.AddMinutes(15);
    }
}
