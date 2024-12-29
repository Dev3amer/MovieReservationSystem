namespace MovieReservationSystem.Core.Features.Movies.Queries.Results.Shared
{
    public class ShowTimeInMovieResponse
    {
        public int ShowTimeId { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
