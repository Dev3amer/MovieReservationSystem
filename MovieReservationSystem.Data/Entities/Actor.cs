namespace MovieReservationSystem.Data.Entities
{
    public class Actor
    {
        public int ActorId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; } = new Person();
        public virtual ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
