namespace MovieReservationSystem.Core.Features.Halls.Queries.Results
{
    public class GetAllHallsResponse
    {
        public int HallId { get; set; }
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
    }
}
