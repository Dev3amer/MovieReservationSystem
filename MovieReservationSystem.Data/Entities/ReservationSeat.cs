namespace MovieReservationSystem.Data.Entities
{
    public class ReservationSeat
    {
        public int ReservationId { get; set; }
        public int SeatId { get; set; }

        public virtual Reservation Reservation { get; set; } = new();
        public virtual Seat Seat { get; set; } = new();
    }
}
