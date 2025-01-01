using MediatR;
using MovieReservationSystem.Core.Features.Seats.Queries.Results;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.Seats.Commands.Models
{
    public class CreateSeatCommand : IRequest<Response<GetSeatByIdResponse>>
    {
        public string SeatNumber { get; set; } = default!;
        public int HallId { get; set; }
        public byte SeatTypeId { get; set; }

        public CreateSeatCommand(string seatNumber, int hallId, byte seatTypeId)
        {
            SeatNumber = seatNumber.Trim();
            HallId = hallId;
            SeatTypeId = seatTypeId;
        }
    }
}
