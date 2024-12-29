namespace MovieReservationSystem.Core.Features.Halls.Queries.Results
{
    public class GetHallByIdResponse
    {
        public int HallId { get; set; }
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
    }
}
