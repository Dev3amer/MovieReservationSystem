namespace MovieReservationSystem.Data.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PosterURL { get; set; } = default!;
        public int DurationInMinutes { get; set; }
        public int ReleaseYear { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public int DirectorId { get; set; }
        public virtual Director Director { get; set; } = new();
        public virtual ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();
        public virtual ICollection<ShowTime> ShowTimes { get; set; } = new HashSet<ShowTime>();
        public virtual ICollection<Actor> Actors { get; set; } = new HashSet<Actor>();
    }
}
