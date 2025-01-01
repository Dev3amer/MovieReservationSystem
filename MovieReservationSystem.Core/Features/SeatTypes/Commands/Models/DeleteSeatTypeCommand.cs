using MediatR;
using MovieReservationSystem.Core.Response;

namespace MovieReservationSystem.Core.Features.SeatTypes.Commands.Models
{
    public class DeleteSeatTypeCommand : IRequest<Response<bool>>
    {
        public byte SeatTypeId { get; set; }
    }
}
