namespace MovieReservationSystem.Data.Entities
{
    public class MovieGenre
    {
        public int MovieId { get; set; }
        public byte GenreId { get; set; }
        public virtual Movie Movie { get; set; } = new();
        public virtual Genre Genre { get; set; } = new();
    }

}
