namespace MovieReservationSystem.Data.Entities
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public int ActorId { get; set; }
        public virtual Movie Movie { get; set; } = new();
        public virtual Actor Actor { get; set; } = new();
    }

}
