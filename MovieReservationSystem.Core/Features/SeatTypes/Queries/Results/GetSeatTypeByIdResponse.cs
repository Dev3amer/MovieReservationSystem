namespace MovieReservationSystem.Core.Features.SeatTypes.Queries.Results
{
    public class GetSeatTypeByIdResponse
    {
        public byte SeatTypeId { get; set; }
        public string TypeName { get; set; } = default!;
        public decimal SeatTypePrice { get; set; }
    }
}
