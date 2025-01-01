using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Data.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public decimal FinalPrice { get; set; }
        public int ShowTimeId { get; set; }
        public string UserId { get; set; } = default!;
        public virtual ShowTime ShowTime { get; set; } = new();
        public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
        public virtual User User { get; set; } = new();

    }
}
