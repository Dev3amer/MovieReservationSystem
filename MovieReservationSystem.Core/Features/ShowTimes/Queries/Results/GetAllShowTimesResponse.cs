namespace MovieReservationSystem.Core.Features.ShowTimes.Queries.Results
{
    public class GetAllShowTimesResponse
    {
        public int ShowTimeId { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public decimal ShowTimePrice { get; set; }
        public string MovieTitle { get; set; } = default!;
        public string HallName { get; set; } = default!;
    }
}
