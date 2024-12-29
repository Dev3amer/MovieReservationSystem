using MovieReservationSystem.Data.Entities.Identity;

namespace MovieReservationSystem.Data.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public decimal FinalPrice { get; set; }
        public int ShowTimeId { get; set; }
        public int SeatId { get; set; }
        public virtual ShowTime ShowTime { get; set; } = new();
        public virtual User User { get; set; } = new();
        public virtual Seat Seat { get; set; } = new();
    }
}
