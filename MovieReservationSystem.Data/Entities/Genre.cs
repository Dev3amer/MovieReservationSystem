namespace MovieReservationSystem.Data.Entities
{
    public class Genre
    {
        public byte GenreId { get; set; }
        public string Name { get; set; } = default!;
        public virtual ICollection<Movie>? Movies { get; set; } = new HashSet<Movie>();
    }
}
