using MediatR;
using MovieReservationSystem.Core.ResponseBases;

namespace MovieReservationSystem.Core.Features.Seats.Commands.Models
{
    public class DeleteSeatCommand : IRequest<Response<bool>>
    {
        public int SeatId { get; set; }
    }
}
