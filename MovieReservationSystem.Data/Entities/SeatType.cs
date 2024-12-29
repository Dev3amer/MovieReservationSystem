namespace MovieReservationSystem.Data.Entities
{
    public class SeatType
    {
        public byte SeatTypeId { get; set; }
        public string TypeName { get; set; } = default!;
        public decimal SeatTypePrice { get; set; }
        public ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
    }

}
