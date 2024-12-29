namespace MovieReservationSystem.Data.Entities
{
    public class Hall
    {
        public int HallId { get; set; }
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
        public ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
        public ICollection<ShowTime> ShowTimes { get; set; } = new HashSet<ShowTime>();
    }
}
