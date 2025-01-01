namespace MovieReservationSystem.Data.Entities
{
    public class Seat
    {
        public int SeatId { get; set; }
        public string SeatNumber { get; set; } = default!;
        public int HallId { get; set; }
        public byte SeatTypeId { get; set; }
        public Hall Hall { get; set; } = new();
        public SeatType SeatType { get; set; } = new();
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
