namespace MovieReservationSystem.Data.Entities
{
    public class ShowTime
    {
        public int ShowTimeId { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal ShowTimePrice { get; set; }
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public virtual Hall Hall { get; set; } = new();
        public virtual Movie Movie { get; set; } = new();
        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}
